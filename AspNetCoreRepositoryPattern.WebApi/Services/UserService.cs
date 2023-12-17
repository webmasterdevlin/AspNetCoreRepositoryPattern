using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Helpers;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreRepositoryPattern.Services;

public class UserService(IOptions<AuthSettings> appSettings, ApplicationDbContext context)
    : IUserService
{
    private readonly AuthSettings _authSettings = appSettings.Value;

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        if (user == null)
            return null;

        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }


    public User? GetById(Guid id) => context.Users.FirstOrDefault(u => u.Id == id);

    private string GenerateJwtToken(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(_authSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("sub", user.Id.ToString()), new Claim("email", user.Email) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}