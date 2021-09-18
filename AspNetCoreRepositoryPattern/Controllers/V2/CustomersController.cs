using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Controllers.Base;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AspNetCoreRepositoryPattern.Controllers.V2
{
    [ApiVersion("2.0")]
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _distributedCache;

        public CustomersController(ApplicationDbContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }
        
        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            const string cacheKey = "customerList";

            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            var customerList = redisCustomerList == null
                ? await StoreCustomerListInRedis(cacheKey)
                : ReadCustomerListFromRedis(redisCustomerList);

            return Ok(customerList);
        }

        private List<Customer> ReadCustomerListFromRedis(byte[] redisCustomerList)
        {
            var serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
            var customerList = JsonConvert.DeserializeObject<List<Customer>>(serializedCustomerList);
            return customerList;
        }
        
        private async Task<List<Customer>> StoreCustomerListInRedis(string cacheKey)
        {
            var customerList = await _context.Customers.ToListAsync();
            var serializedCustomerList = JsonConvert.SerializeObject(customerList);
            var redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            return customerList;
        }
    }
}