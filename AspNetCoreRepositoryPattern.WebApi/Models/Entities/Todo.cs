using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreRepositoryPattern.Models.Entities
{
    [Table("Todos")]
    public class Todo
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(54)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Done is required")]
        public bool Done { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
