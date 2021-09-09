using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Controllers;
using FluentAssertions;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests
{
    public class JobControllerTest
    {
        private readonly Mock<IJobService> _mockRepo;
        private readonly JobController _controller;

        /* xUnit.net creates a new instance of the test class for every test it contains.
           This allows you to put the setup code you need in the constructor of this TodosControllerTest class.*/
        public JobControllerTest()
        {
            _mockRepo = new Mock<IJobService>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockJobManager = new Mock<IRecurringJobManager>();
             
            _controller = new JobController(_mockRepo.Object, mockJobClient.Object, mockJobManager.Object);
        }
        [Fact]
        public void FireAndForgetJobTest()
        {
            //arrange
            _mockRepo.Setup(service => service.FireAndForgetJob()).Verifiable();
            
            //act
            var result = _controller.CreateFireAndForgetJob();
            var response = (OkResult)result;
            
            //assert
            Assert.Equal(200, response.StatusCode);
            
            /* Fluent Assertions version */
            response.StatusCode.Should().Be(200);
        }
        [Fact]
        public void RecurringJobTest()
        {
            //arrange
            _mockRepo.Setup(service => service.RecurringJob()).Verifiable();

            //act
            var result = _controller.CreateRecurringJob();
            var response = (OkResult)result;
            
            //assert
            Assert.Equal(200, response.StatusCode);
            
            /* Fluent Assertions version */
            response.StatusCode.Should().Be(200);
        }
        [Fact]
        public void DelayedJobTest()
        {
            //arrange
            _mockRepo.Setup(service => service.DelayedJob()).Verifiable();
            
            //act
            var result = _controller.CreateDelayedJob();
            var response = (OkResult)result;
            
            //assert
            Assert.Equal(200, response.StatusCode);
            
            /* Fluent Assertions version */
            response.StatusCode.Should().Be(200);
        }
    }
}