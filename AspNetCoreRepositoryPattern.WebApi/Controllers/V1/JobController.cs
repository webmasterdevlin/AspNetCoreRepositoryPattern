using AspNetCoreRepositoryPattern.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.V1;

[ApiVersion("1.0", Deprecated = true)]
[Route("api/job")]
public class JobController : ApiController
{
    [HttpGet("fire-and-forget-job")]
    public ActionResult CreateFireAndForgetJob() => Ok(new {message="a sample response from version 1"});

    [HttpGet("delayed-job")]
    public ActionResult CreateDelayedJob() => Ok(new {message="a sample response from version 1"});

    [HttpGet("recurring-job")]
    public ActionResult CreateRecurringJob() => Ok(new {message="a sample response from version 1"});

    [HttpGet("continuation-job")]
    public ActionResult CreateContinuationJob() => Ok(new {message="a sample response from version 1"});
}