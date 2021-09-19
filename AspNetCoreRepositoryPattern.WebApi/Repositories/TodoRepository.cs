using System;
using System.Collections.Generic;
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
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TodoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync();
                var todoDtos = _mapper.Map<IEnumerable<TodoDto>>(todos);
            
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
                var todo = await _context.Todos.FindAsync(id);
                var todoDto = _mapper.Map<TodoDto>(todo);
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
                _context.Todos.Add(todo);

                await _context.SaveChangesAsync();

                var todoDto = _mapper.Map<TodoDto>(todo);
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
            
                _context.Remove(await _context.Todos.FindAsync(id));
                await _context.SaveChangesAsync();
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

                _context.Update(todo);
                await _context.SaveChangesAsync();
                var todoDto = _mapper.Map<TodoDto>(todo);
                return todoDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ExistsAsync(Guid id) => await _context.Todos.AnyAsync(t => t.Id == id);
    }
}
