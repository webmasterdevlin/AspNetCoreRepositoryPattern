using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRepositoryPattern.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Author is required")]
        [MinLength(3)]
        [MaxLength(32)]
        public string Author { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3)]
        [MaxLength(90)]
        public string Title { get; set; }
        [MinLength(12)]
        [MaxLength(180)]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}