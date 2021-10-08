using AspNetCoreRepositoryPattern.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.Base
{
    [Authorize]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        
    }
}