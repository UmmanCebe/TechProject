using Core.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechCareer.API.Controllers;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Service.Abstracts;

namespace Tests
{
    [TestFixture]
    public class VideoEducationsControllerTests
    {
        private Mock<IVideoEducationService> _mockVideoEducationService;
        private Mock<IVideoEducationRepository> _mockRepository;
        private VideoEducationsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockVideoEducationService = new Mock<IVideoEducationService>();
            _controller = new VideoEducationsController(_mockVideoEducationService.Object);
            _mockRepository = new Mock<IVideoEducationRepository>();
        }

        [Test]
        public async Task GetList_ShouldReturnOkResultWithVideoEducations()
        {
            // Arrange
            var videoEducations = new List<VideoEducationResponse>
            {
                new VideoEducationResponse { Id = 1, Title = "Video 1" },
                new VideoEducationResponse { Id = 2, Title = "Video 2" }
            };
            _mockVideoEducationService.Setup(service => service.GetListAsync(null,null,true,false,false,default))
                .ReturnsAsync(videoEducations);

            // Act
            var result = await _controller.GetList();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(videoEducations, okResult.Value);
        }

        [Test]
        public async Task GetListByInstructor_ShouldReturnOkResultWithVideoEducations()
        {
            // Arrange
            var instructorId = Guid.NewGuid();
            var videoEducations = new List<VideoEducationResponse>
            {
                new VideoEducationResponse { Id = 1, Title = "Video 1" },
                new VideoEducationResponse { Id = 2, Title = "Video 2" }
            };
            _mockVideoEducationService.Setup(service => service.GetListAsync(
                u => u.InstructorId == instructorId,null,true,false,false,default))
                .ReturnsAsync(videoEducations);

            // Act
            var result = await _controller.GetListByInstructor(instructorId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(videoEducations, okResult.Value);
        }

        [Test]
        public async Task Get_ShouldReturnOkResultWithVideoEducation_WhenVideoEducationExists()
        {
            // Arrange
            var videoEducationId = 1;
            var videoEducation = new VideoEducationResponse { Id = videoEducationId, Title = "Video 1" };
            _mockVideoEducationService.Setup(service => service.GetAsync(u => u.Id == videoEducationId,true,false,true,default))
                .ReturnsAsync(videoEducation);

            // Act
            var result = await _controller.Get(videoEducationId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(videoEducation, okResult.Value);
        }

        [Test]
        public async Task Add_ShouldReturnOkResultWithCreatedVideoEducation()
        {
            // Arrange
            var request = new VideoEducationCreateRequest { Title = "New Video" };
            var createdVideoEducation = new VideoEducationResponse { Id = 1, Title = "New Video" };
            _mockVideoEducationService.Setup(service => service.AddAsync(request))
                .ReturnsAsync(createdVideoEducation);

            // Act
            var result = await _controller.Add(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(createdVideoEducation, okResult.Value);
        }

        [Test]
        public async Task Update_ShouldReturnOkResultWithUpdatedVideoEducation()
        {
            // Arrange
            var videoEducationId = 1;
            var request = new VideoEducationUpdateRequest { Title = "Updated Video" };
            var updatedVideoEducation = new VideoEducationResponse { Id = videoEducationId, Title = "Updated Video" };
            _mockVideoEducationService.Setup(service => service.UpdateAsync(videoEducationId, request))
                .ReturnsAsync(updatedVideoEducation);

            // Act
            var result = await _controller.Update(videoEducationId, request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedVideoEducation, okResult.Value);
        }

        [Test]
        public async Task Delete_ShouldReturnOkResultWithDeletedVideoEducation()
        {
            // Arrange
            var videoEducationId = 1;
            var deletedVideoEducation = new VideoEducationResponse { Id = videoEducationId, Title = "Deleted Video" };
            _mockVideoEducationService.Setup(service => service.DeleteAsync(videoEducationId, false))
                .ReturnsAsync(deletedVideoEducation);

            // Act
            var result = await _controller.Delete(videoEducationId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(deletedVideoEducation, okResult.Value);
        }
    }
}
