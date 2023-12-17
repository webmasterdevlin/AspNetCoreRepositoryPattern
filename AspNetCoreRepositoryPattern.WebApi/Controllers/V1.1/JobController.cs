using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers.Base;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.V1._1;

[ApiVersion("1.1")]
[Route("api/job")]
public class JobController(
    IJobService jobService,
    IBackgroundJobClient backgroundJobClient,
    IRecurringJobManager recurringJobManager)
    : ApiController
{
    [HttpGet("fire-and-forget-job")]
    public ActionResult CreateFireAndForgetJob()
    {
        backgroundJobClient.Enqueue(() => jobService.FireAndForgetJob());
        return Ok();
    }
        
    [HttpGet("delayed-job")]
    public ActionResult CreateDelayedJob()
    {
        backgroundJobClient.Schedule(() => jobService.DelayedJob(), TimeSpan.FromSeconds(60));
        return Ok();
    }
        
    [HttpGet("recurring-job")]
    public ActionResult CreateRecurringJob()
    {
        recurringJobManager.AddOrUpdate("jobId", () => jobService.RecurringJob(), Cron.Minutely);
        return Ok();
    }
        
    [HttpGet("continuation-job")]
    public ActionResult CreateContinuationJob()
    {
        var parentJobId = backgroundJobClient.Enqueue(() => jobService.FireAndForgetJob());
        backgroundJobClient.ContinueJobWith(parentJobId, () => jobService.ContinuationJob());
            
        return Ok();
    }
}