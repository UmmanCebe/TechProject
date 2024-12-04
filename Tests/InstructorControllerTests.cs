using Core.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechCareer.API.Controllers;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Service.Abstracts;

namespace Tests
{
    [TestFixture]
    public class InstructorsControllerTests
    {
        private Mock<IInstructorService> _mockInstructorService;
        private InstructorsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockInstructorService = new Mock<IInstructorService>();
            _controller = new InstructorsController(_mockInstructorService.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnOkResultWithInstructors()
        {
            // Arrange
            var instructors = new List<InstructorResponseDto>
            {
                new InstructorResponseDto { Id = Guid.NewGuid(), Name = "John Doe" },
                new InstructorResponseDto { Id = Guid.NewGuid(), Name = "Jane Smith" }
            };
            _mockInstructorService.Setup(service => service.GetAllAsync(null, null, false, false, true, default))
                .ReturnsAsync(instructors);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(instructors, okResult.Value);
        }

        [Test]
        public async Task GetOne_ShouldReturnOkResultWithInstructor_WhenInstructorExists()
        {
            // Arrange
            var instructorId = Guid.NewGuid();
            var instructor = new InstructorResponseDto { Id = instructorId, Name = "John Doe" };
            _mockInstructorService.Setup(service => service.GetOneAsync(i => i.Id == instructorId, false, false, true, default))
                .ReturnsAsync(instructor);

            // Act
            var result = await _controller.GetOne(instructorId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(instructor, okResult.Value);
        }

        [Test]
        public async Task Add_ShouldReturnOkResultWithCreatedInstructor()
        {
            // Arrange
            var dto = new InstructorCreateRequestDto { Name = "John Doe" };
            var createdInstructor = new InstructorResponseDto { Id = Guid.NewGuid(), Name = "John Doe" };
            _mockInstructorService.Setup(service => service.AddAsync(dto))
                .ReturnsAsync(createdInstructor);

            // Act
            var result = await _controller.Add(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(createdInstructor, okResult.Value);
        }

        [Test]
        public async Task Update_ShouldReturnOkResultWithUpdatedInstructor()
        {
            // Arrange
            var instructorId = Guid.NewGuid();
            var dto = new InstructorUpdateRequestDto { Name = "Updated Name" };
            var updatedInstructor = new InstructorResponseDto { Id = instructorId, Name = "Updated Name" };
            _mockInstructorService.Setup(service => service.UpdateAsync(dto, instructorId))
                .ReturnsAsync(updatedInstructor);

            // Act
            var result = await _controller.Update(instructorId, dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedInstructor, okResult.Value);
        }

        [Test]
        public async Task Delete_ShouldReturnOkResultWithDeletedInstructor()
        {
            // Arrange
            var instructorId = Guid.NewGuid();
            var deletedInstructor = new InstructorResponseDto { Id = instructorId, Name = "Deleted Name" };
            _mockInstructorService.Setup(service => service.DeleteAsync(instructorId, false))
                .ReturnsAsync(deletedInstructor);

            // Act
            var result = await _controller.Delete(instructorId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(deletedInstructor, okResult.Value);
        }

        [Test]
        public async Task GetPaginate_ShouldReturnOkResultWithPaginatedInstructors()
        {
            // Arrange
            var paginatedInstructors = new Paginate<InstructorResponseDto>
            {
                Items = new List<InstructorResponseDto>
                {
                    new InstructorResponseDto { Id = Guid.NewGuid(), Name = "John Doe" },
                    new InstructorResponseDto { Id = Guid.NewGuid(), Name = "Jane Smith" }
                },
            };
            _mockInstructorService.Setup(service => service.GetPaginateAsync(null, null, false, 0, 10, false, true, default))
                .ReturnsAsync(paginatedInstructors);

            // Act
            var result = await _controller.GetPaginate(0, 10);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(paginatedInstructors, okResult.Value);
        }
    }
}
