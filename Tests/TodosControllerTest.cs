using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers;
using AspNetCoreRepositoryPattern.Data;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests
{
    public class TodosControllerTest
    {
        [Fact]
        public async Task GetTodosTest()
        {
            //arrange
            var mockRepo = new Mock<ITodoRepository>();
            var mockTodoDtos = MockData.GetAll();
            mockRepo.Setup(repository => repository
                .GetAllAsync())
                .Returns(Task.FromResult(mockTodoDtos));
            var controller = new TodosController(mockRepo.Object);

            //act
            var result = controller.GetTodos();
            var actionResult = await result;
            var response = (OkObjectResult)actionResult;
            var todos = response.Value as IEnumerable<TodoDto>;
            
            //assert
            await Assert.IsType<Task<ActionResult>>(result);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(todos);
            Assert.Equal(4,todos.Count());
        }
        
        [Theory]
        [InlineData("117366b8-3541-4ac5-8732-860d698e26a2", "45055aea-2a27-4008-afb6-ede9d69710ff")]
        public async Task GetTodoByIdTest(string validGuid, string invalidGuid)
        {
            //arrange
            var mockRepo = new Mock<ITodoRepository>();
            var validItemGuid = new Guid(validGuid);
            var mockTodoDto = MockData.GetAll().FirstOrDefault(t => t.Id == validItemGuid);
            mockRepo.Setup(repository => repository
                    .GetByIdAsync(validItemGuid))
                    .Returns(Task.FromResult(mockTodoDto));
            var controller = new TodosController(mockRepo.Object);
            
            //act
            var result = controller.GetTodoById(validItemGuid);
            var actionResult = await result;
            var response = (OkObjectResult)actionResult;
            var todo = response.Value as TodoDto;
            
            //assert
            await Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(todo);
            
            
            //arrange
            var invalidItemGuid = new Guid(invalidGuid);
            var mockInvalidTodoDto = MockData.GetAll().FirstOrDefault(t => t.Id == invalidItemGuid);
            
            mockRepo.Setup(repository => repository
                    .GetByIdAsync(invalidItemGuid))
                    .Returns(Task.FromResult(mockInvalidTodoDto));
                
            //act
            var invalidResult = controller.GetTodoById(invalidItemGuid);
            var invalidActionResult = await invalidResult;
            var notFoundResponse = (NotFoundResult)invalidActionResult;

            //assert
            Assert.Equal(404, notFoundResponse.StatusCode);
        }

        [Theory]
        [InlineData("117366b8-3541-4ac5-8732-860d698e26a2")]
        public async Task DeleteTodoTest(string validGuid)
        {
            //arrange
            var mockRepo = new Mock<ITodoRepository>();
            var mockTodoDtos = MockData.GetAll();
            mockRepo.Setup(repository => repository
                    .GetAllAsync())
                .Returns(Task.FromResult(mockTodoDtos));
            var controller = new TodosController(mockRepo.Object);
            var validTodoGuid = new Guid(validGuid);
            
            //act
            var result = controller.DeleteTodo(validTodoGuid);
            var actionResult = await result;
            
            var response = (NoContentResult)actionResult;
            
            //assert
            await Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal(204, response.StatusCode);
        }

        [Fact]
        public async Task PostTodoTest()
        {
            //arrange
            var mockRepo = new Mock<ITodoRepository>();
            var controller = new TodosController(mockRepo.Object);
            var newValidTodo = new Todo
            { 
                Name = "Booking",
                Done = false
            };

            //act
            var actionResult =  controller.PostTodo(newValidTodo);
            var response = await actionResult;
            
            //assert
            Assert.NotNull(response.Value);
            Assert.IsType<Guid>(response.Value.Id);
        }

        public async Task PutTodoTest()
        {
            
        }
    }
}