using AspNetCoreRepositoryPattern.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.Base
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true), ApiVersion("1.1"), ApiVersion("2.0")]
    public abstract class ApiController : ControllerBase
    {
        
    }
}