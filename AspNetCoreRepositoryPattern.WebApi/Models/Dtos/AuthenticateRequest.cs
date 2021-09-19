using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRepositoryPattern.Models.Dtos
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}