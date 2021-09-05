using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Entities;
using AutoMapper;
using AspNetCoreRepositoryPattern.Models.Dtos;

namespace AspNetCoreRepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        // Database CRUD operations in the controller
        private readonly ApplicationDbContext _context;

        // for Repository Pattern
        private readonly ITodoRepository _repo;

        public TodosController(ApplicationDbContext context, IMapper mapper, ITodoRepository repo)
        {
            _mapper = mapper;
            _context = context;
            _repo = repo;
        }

        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult> GetTodos()
        {
            var todos = await _repo.GetAllAsync();
            var todoDtos = _mapper.Map<IEnumerable<TodoDto>>(todos);

            return Ok(todoDtos);
        }

        // GET: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/todos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodos", new { id = todo.Id }, todo);
        }

        // DELETE: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
