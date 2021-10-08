using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreRepositoryPattern.Models.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(2)]
        [MaxLength(54)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2)]
        [MaxLength(54)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Mobile is required")]
        [MinLength(5)]
        [MaxLength(20)]
        public string Mobile { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [MinLength(5)]
        [MaxLength(54)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}