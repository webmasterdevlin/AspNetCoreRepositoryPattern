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
        private readonly Mock<IBackgroundJobClient> _mockJobClient;
        private readonly Mock<IRecurringJobManager> _mockJobManager;

        /* xUnit.net creates a new instance of the test class for every test it contains.
           This allows you to put the setup code you need in the constructor of this TodosControllerTest class.*/
        public JobControllerTest()
        {
             _mockRepo = new Mock<IJobService>();
             _mockJobClient = new Mock<IBackgroundJobClient>();
             _mockJobManager = new Mock<IRecurringJobManager>();
        }
        [Fact]
        public void FireAndForgetJobTest()
        {
            //arrange
            _mockRepo.Setup(service => service.FireAndForgetJob()).Verifiable();
            var controller = new JobController(_mockRepo.Object, _mockJobClient.Object, _mockJobManager.Object);
            
            //act
            var result = controller.CreateFireAndForgetJob();
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
            var controller = new JobController(_mockRepo.Object, _mockJobClient.Object, _mockJobManager.Object);
            
            //act
            var result = controller.CreateRecurringJob();
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
            var controller = new JobController(_mockRepo.Object, _mockJobClient.Object, _mockJobManager.Object);
            
            //act
            var result = controller.CreateDelayedJob();
            var response = (OkResult)result;
            
            //assert
            Assert.Equal(200, response.StatusCode);
            
            /* Fluent Assertions version */
            response.StatusCode.Should().Be(200);
        }
    }
}