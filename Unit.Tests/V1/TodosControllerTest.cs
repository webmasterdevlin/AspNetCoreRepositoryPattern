using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers.V1;
using AspNetCoreRepositoryPattern.Data;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Unit.Tests.V1
{
    public class TodosControllerTest
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodosController _controller;

        /* xUnit.net creates a new instance of the test class for every test it contains.
         This allows you to put the setup code you need in the constructor of this TodosControllerTest class.*/
        public TodosControllerTest()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _controller = new TodosController(_mockRepo.Object);
        }
        
        [Fact]
        public async Task GetTodosTest()
        {
            //arrange
            var mockTodoDtos = MockData.GetAllTodos();
            _mockRepo.Setup(repository => repository
                .GetAllAsync())
                .Returns(Task.FromResult(mockTodoDtos));
      

            //act
            var result = await _controller.GetTodos();
            var response = (OkObjectResult)result;
            var todos = response.Value as IEnumerable<TodoDto>;
            
            //assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.NotNull(todos);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(4,todos.Count());
            
            /* Fluent Assertions version */
            result.Should().BeAssignableTo<ActionResult>();
            todos.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            todos.Count().Should().Be(4);
        }
        
        [Theory]
        [InlineData("117366b8-3541-4ac5-8732-860d698e26a2", "45055aea-2a27-4008-afb6-ede9d69710ff")]
        public async Task GetTodoByIdTest(string validGuid, string invalidGuid)
        {
            //arrange
            var validItemGuid = new Guid(validGuid);
            var mockTodoDto = MockData.GetAllTodos().FirstOrDefault(t => t.Id == validItemGuid);
            _mockRepo.Setup(repository => repository
                    .GetByIdAsync(validItemGuid))
                    .Returns(Task.FromResult(mockTodoDto));

            //act
            var result = await _controller.GetTodoById(validItemGuid);
            var response = (OkObjectResult)result;
            var todo = response.Value as TodoDto;
            
            //assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(todo);
            
            /* Fluent Assertions version */
            result.Should().BeAssignableTo<IActionResult>();
            response.StatusCode.Should().Be(200);
            todo.Should().NotBeNull();
            
            //arrange
            var invalidItemGuid = new Guid(invalidGuid);
            var mockInvalidTodoDto = MockData.GetAllTodos().FirstOrDefault(t => t.Id == invalidItemGuid);
            
            _mockRepo.Setup(repository => repository
                     .GetByIdAsync(invalidItemGuid))
                     .Returns(Task.FromResult(mockInvalidTodoDto));
                
            //act
            var invalidResult = await _controller.GetTodoById(invalidItemGuid);
            var notFoundResponse = (NotFoundResult)invalidResult;

            //assert
            Assert.Equal(404, notFoundResponse.StatusCode);
            
            /* Fluent Assertions version */
            notFoundResponse.StatusCode.Should().Be(404);
        }

        [Theory]
        [InlineData("117366b8-3541-4ac5-8732-860d698e26a2")]
        public async Task DeleteTodoTest(string validGuid)
        {
            //arrange
            var mockTodoDtos = MockData.GetAllTodos();
            _mockRepo.Setup(repository => repository
                     .GetAllAsync())
                     .Returns(Task.FromResult(mockTodoDtos));
            var validTodoGuid = new Guid(validGuid);
            
            //act
            var result = await _controller.DeleteTodo(validTodoGuid);
            var response = (NoContentResult)result;
            
            //assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(204, response.StatusCode);
            
            /* Fluent Assertions version */
            result.Should().BeAssignableTo<IActionResult>();
            response.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task PostTodoTest()
        {
            //arrange
            var mockTodoDto = MockData.GetOneTodoDto();
            var newTodo = new Todo
            { 
                Name = mockTodoDto.Name,
                Done = mockTodoDto.Done
            };
            
            _mockRepo.Setup(repository => repository
                     .CreateAsync(newTodo))
                     .Returns(Task.FromResult(mockTodoDto));

            //act
            var result = await _controller.PostTodo(newTodo);
            var response = (OkObjectResult)result;
            var todoDto = response.Value as TodoDto;
            
            //assert
            Assert.NotNull(todoDto);
            Assert.IsType<Guid>(todoDto.Id);
            Assert.Equal(200, response.StatusCode);
            
            /* Fluent Assertions version */
            todoDto.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task PutTodoTest()
        {
            //arrange
            var mockTodo = MockData.GetOneTodo();
            var mockTodoDto = MockData.GetOneTodoDto();
            mockTodo.Done = true;
            
            _mockRepo.Setup(repository => repository
                     .UpdateAsync(mockTodo))
                     .Returns(Task.FromResult(mockTodoDto));
            
            //act
            var result = await _controller.PutTodo(mockTodo.Id, mockTodo);
            var response = (NoContentResult)result;
            
            //assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(204, response.StatusCode);
            
            /* Fluent Assertions version */
            result.Should().BeAssignableTo<IActionResult>();
            response.StatusCode.Should().Be(204);
        }
    }
}