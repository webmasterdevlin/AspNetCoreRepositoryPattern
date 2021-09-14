using System;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Controllers
{
    /*
     * Using repository pattern.
     */
    public class TodosController : ApiController
    {
        private readonly ITodoRepository _repo;

        public TodosController(ITodoRepository repo)
        {
            _repo = repo;
        }

        // GET: api/todos
        [HttpGet]
        [ApiVersion("2.0")]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _repo.GetAllAsync();
            var response = Ok(todos);
            
            return response;
        }

        // GET: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpGet("{id}")]
        [ApiVersion("2.0")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var todo = await _repo.GetByIdAsync(id);

            if (todo == null)
                return NotFound();
            
            var response = Ok(todo);
            
            return response;
        }
        
        // DELETE: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpDelete("{id:guid}")]
        [ApiVersion("2.0")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            await _repo.DeleteAsync(id);

            return NoContent();
        }
        
        // POST: api/todos
        [HttpPost]
        [ApiVersion("2.0")]
        public async Task<IActionResult> PostTodo(Todo todo)
        {
            var todoDto = await _repo.CreateAsync(todo);
            var response = Ok(todoDto);

            return response;
        }

        // PUT: api/todos/5074551d-ebd7-454c-9436-0c363b4e36b3
        [HttpPut("{id:guid}")]
        [ApiVersion("2.0")]
        public async Task<IActionResult> PutTodo(Guid id, Todo todo)
        {
            if (id != todo.Id) 
                return BadRequest();
            
            await _repo.UpdateAsync(todo);
            
            return NoContent();
        }
    }
}
