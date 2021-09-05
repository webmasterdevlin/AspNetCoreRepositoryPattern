using System;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _repo;

        public TodosController(ITodoRepository repo)
        {
            _repo = repo;
        }

        // GET: api/todos
        [HttpGet]
        public ActionResult GetTodos()
        {
            var todos = _repo.GetAllAsync();
            
            return Ok(todos);
        }

        // GET: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(Guid id)
        {
            var todo = await _repo.GetByIdAsync(id);

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

            try
            {
                await _repo.UpdateAsync(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await TodoExists(id);
                if (!exists)
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
            await _repo.CreateAsync(todo);

            return CreatedAtAction("GetTodos", new { id = todo.Id }, todo);
        }

        // DELETE: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _repo.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(todo.Id);

            return NoContent();
        }

        private async Task<bool> TodoExists(Guid id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}
