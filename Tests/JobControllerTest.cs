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
        [Fact]
        public void FireAndForgetJobTest()
        {
            //arrange
            var mockRepo = new Mock<IJobService>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockJobManager = new Mock<IRecurringJobManager>();
            mockRepo.Setup(service => service.FireAndForgetJob()).Verifiable();
            var controller = new JobController(mockRepo.Object, mockJobClient.Object, mockJobManager.Object);
            
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
            var mockRepo = new Mock<IJobService>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockJobManager = new Mock<IRecurringJobManager>();
            mockRepo.Setup(service => service.RecurringJob()).Verifiable();
            var controller = new JobController(mockRepo.Object, mockJobClient.Object, mockJobManager.Object);
            
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
            var mockRepo = new Mock<IJobService>();
            var mockJobClient = new Mock<IBackgroundJobClient>();
            var mockJobManager = new Mock<IRecurringJobManager>();
            mockRepo.Setup(service => service.DelayedJob()).Verifiable();
            var controller = new JobController(mockRepo.Object, mockJobClient.Object, mockJobManager.Object);
            
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