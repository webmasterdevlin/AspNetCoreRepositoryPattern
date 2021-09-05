using AutoMapper;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
        }
    }
}