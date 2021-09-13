using System;
using AspNetCoreRepositoryPattern.Contracts;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true), ApiVersion("1.1"), ApiVersion("2.0")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobController(IJobService jobService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }
        
        [HttpGet("fire-and-forget-job")]
        [ApiVersion("1.1")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            return Ok();
        }
        
        [HttpGet("delayed-job")]
        [ApiVersion("1.1")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }
        
        [HttpGet("recurring-job")]
        [ApiVersion("1.1")]
        public ActionResult CreateRecurringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.RecurringJob(), Cron.Minutely);
            return Ok();
        }
        
        [HttpGet("continuation-job")]
        [ApiVersion("1.1")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.ContinuationJob());
            
            return Ok();
        }
    }
}