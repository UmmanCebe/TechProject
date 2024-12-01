using Microsoft.AspNetCore.Mvc;
using Moq;
using TechCareer.API.Controllers;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Service.Abstracts;

namespace TechCareer.Tests.Controllers
{
    [TestFixture]
    public class VideoEducationsControllerTests
    {
        private Mock<IVideoEducationService> _mockService;
        private VideoEducationsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IVideoEducationService>();
            _controller = new VideoEducationsController(_mockService.Object);
        }

        #region Add

        [Test]
        public async Task Add_WhenCalled_ReturnsOkWithCreatedVideo()
        {
            // Arrange
            var request = new VideoEducationCreateRequest { Title = "New Video" };
            var response = new VideoEducationResponse { Id = 1, Title = "New Video" };
            _mockService.Setup(s => s.AddAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Add(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        #endregion

        #region Update

        [Test]
        public async Task Update_WhenCalled_ReturnsOkWithUpdatedVideo()
        {
            // Arrange
            var id = 1;
            var request = new VideoEducationUpdateRequest { Title = "Updated Title" };
            var response = new VideoEducationResponse { Id = id, Title = "Updated Title" };
            _mockService.Setup(s => s.UpdateAsync(id, request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Update(id, request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        #endregion

        #region Delete

        [Test]
        public async Task Delete_WhenCalled_ReturnsOkWithDeleteConfirmation()
        {
            // Arrange
            var id = 1;
            var response = new VideoEducationResponse { Id = id, Title = "Deleted Video" };
            _mockService.Setup(s => s.DeleteAsync(id, false)).ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        #endregion
    }
}
