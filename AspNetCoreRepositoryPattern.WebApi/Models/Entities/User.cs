using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreRepositoryPattern.Models.Entities
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Firstname is required")]
        [MinLength(2)]
        [MaxLength(54)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Lastname is required")]
        [MinLength(2)]
        [MaxLength(54)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [MinLength(3)]
        [MaxLength(54)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(54)]
        public string Password { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}