using AspNetCoreRepositoryPattern.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.V1;

[ApiVersion("1.0", Deprecated = true)]
[Route("api/customers")]
public class CustomersController : ApiController
{
    // GET: api/customers
    [HttpGet]
    public ActionResult GetAllCustomersUsingRedisCache() => Ok(new {message="a sample response from version 1"});
}