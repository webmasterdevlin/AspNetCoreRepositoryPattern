using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Contracts;

public interface ITodoRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<TodoDto>> GetAllAsync();
    Task<TodoDto> GetByIdAsync(Guid id);
    Task<TodoDto> CreateAsync(Todo todo);
    Task<TodoDto> UpdateAsync(Todo todo);
    Task DeleteAsync(Guid id);
}