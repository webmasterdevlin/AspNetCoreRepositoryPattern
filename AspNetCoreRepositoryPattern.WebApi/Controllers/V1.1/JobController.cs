using System;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers.Base;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.V1._1
{
    [ApiVersion("1.1")]
    [Route("api/job")]
    public class JobController : ApiController
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
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            return Ok();
        }
        
        [HttpGet("delayed-job")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }
        
        [HttpGet("recurring-job")]
        public ActionResult CreateRecurringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.RecurringJob(), Cron.Minutely);
            return Ok();
        }
        
        [HttpGet("continuation-job")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.ContinuationJob());
            
            return Ok();
        }
    }
}