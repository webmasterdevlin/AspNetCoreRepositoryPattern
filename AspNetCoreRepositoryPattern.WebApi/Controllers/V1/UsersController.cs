using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRepositoryPattern.Controllers.V1;

[ApiVersion("1.0")]
[AllowAnonymous]
[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("auth")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
}