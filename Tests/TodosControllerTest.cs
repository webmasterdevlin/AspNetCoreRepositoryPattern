using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers;
using AspNetCoreRepositoryPattern.Data;
using AspNetCoreRepositoryPattern.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests
{
    public class TodosControllerTest
    {
        [Fact]
        public void GetAllUnitTest()
        {
            //arrange
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repository => repository.GetAllAsync()).Returns(Task.FromResult(MockData.GetAll()));

            var controller = new TodosController(mockRepo.Object);

            //act
            var result = controller.GetTodos();
            var okObjectResult = (OkObjectResult)result;
            var statusCode = okObjectResult.StatusCode;
            var todos = okObjectResult.Value as Task<IEnumerable<TodoDto>>;
            
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCode);
            Assert.NotNull(okObjectResult.Value);
            Assert.Equal(4,todos?.Result.Count() );
        }
    }
}