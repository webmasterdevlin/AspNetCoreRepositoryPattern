using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Contracts
{
    public interface ITodoRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo> UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);
    }
}
