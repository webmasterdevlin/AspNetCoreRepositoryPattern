using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreRepositoryPattern.Models.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(54)]
        public string Name { get; set; }
        [Required]
        public bool Done { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
