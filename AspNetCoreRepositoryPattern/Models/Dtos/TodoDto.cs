using System;
namespace AspNetCoreRepositoryPattern.Models.Dtos
{
    public class TodoDto
    {
        public TodoDto()
        {
        }

        public TodoDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }
}
