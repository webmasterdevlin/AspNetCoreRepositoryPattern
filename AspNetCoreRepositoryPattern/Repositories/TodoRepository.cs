using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;
        public TodoRepository(TodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            var todos = await _context.Todos.ToListAsync();
            var todoDtos = _mapper.Map<IEnumerable<TodoDto>>(todos);
            
            return todoDtos;
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            _context.Todos.Add(todo);

            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task DeleteAsync(Guid id)
        {
            _ = _context.Remove(await _context.Todos.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Todos.AnyAsync(t => t.Id == id);
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            _context.Update(todo);
            await _context.SaveChangesAsync();
            
            return todo;
        }
    }
}
