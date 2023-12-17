using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Repositories;

public class TodoRepository(ApplicationDbContext context, IMapper mapper) : ITodoRepository
{
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        try
        {
            var todos = await context.Todos.ToListAsync();
            var todoDtos = mapper.Map<IEnumerable<TodoDto>>(todos);
            
            return todoDtos;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
        
    public async Task<TodoDto> GetByIdAsync(Guid id)
    {
        try
        {
            var todo = await context.Todos.FindAsync(id);
            var todoDto = mapper.Map<TodoDto>(todo);
            return todoDto;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<TodoDto> CreateAsync(Todo todo)
    {
        try
        {
            todo.Id = Guid.NewGuid();
            context.Todos.Add(todo);

            await context.SaveChangesAsync();

            var todoDto = mapper.Map<TodoDto>(todo);
            return todoDto;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var exists = await ExistsAsync(id);
            if (!exists) throw new Exception("Not Found");
            
            context.Remove(await context.Todos.FindAsync(id));
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<TodoDto> UpdateAsync(Todo todo)
    {
        try
        {
            var exists = await ExistsAsync(todo.Id);

            if (!exists) throw new Exception("Not Found");

            context.Update(todo);
            await context.SaveChangesAsync();
            var todoDto = mapper.Map<TodoDto>(todo);
            return todoDto;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ExistsAsync(Guid id) => await context.Todos.AnyAsync(t => t.Id == id);
}