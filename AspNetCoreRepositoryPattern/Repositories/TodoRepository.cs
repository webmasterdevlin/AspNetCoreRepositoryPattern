using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
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
