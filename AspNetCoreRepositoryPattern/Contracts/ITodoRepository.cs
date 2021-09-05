using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Contracts
{
    public interface ITodoRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto> GetByIdAsync(Guid id);
        Task<TodoDto> CreateAsync(Todo todo);
        Task<TodoDto> UpdateAsync(TodoDto todo);
        Task DeleteAsync(Guid id);
    }
}
