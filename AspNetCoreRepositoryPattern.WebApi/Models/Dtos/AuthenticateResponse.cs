using System;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Models.Dtos
{
    public class AuthenticateResponse(User user, string token)
    {
        public Guid Id { get; set; } = user.Id;
        public string FirstName { get; set; } = user.FirstName;
        public string LastName { get; set; } = user.LastName;
        public string Email { get; set; } = user.Email;
        public string Token { get; set; } = token;
    }
}