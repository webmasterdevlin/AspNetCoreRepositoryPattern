using System.Collections.Generic;
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
            mockRepo.Setup(repository => repository.GetAll()).Returns(MockData.GetAll());

            var controller = new TodosController(mockRepo.Object);

            //act
            var result = controller.GetTodos();

            //assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var viewResultTodos = Assert.IsAssignableFrom<List<TodoDto>>(okObjectResult.Value);
            Assert.Equal(4, viewResultTodos.Count);
        }
        
    }
}