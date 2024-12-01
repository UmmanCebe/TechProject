using System.Linq.Expressions;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Moq;
using NUnit.Framework;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Rules;

namespace Tests
{
    [TestFixture]
    public class VideoEducationServiceTests
    {
        private Mock<IVideoEducationRepository> _mockRepository;
        private Mock<VideoEducationBusinessRules> _mockBusinessRules;
        private Mock<IMapper> _mockMapper;
        private VideoEducationService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IVideoEducationRepository>();
            _mockBusinessRules = new Mock<VideoEducationBusinessRules>(_mockRepository.Object);
            _mockMapper = new Mock<IMapper>();
            _service = new VideoEducationService(_mockRepository.Object, _mockBusinessRules.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAsync_WhenVideoEducationExists_ReturnsMappedResponse()
        {
            // Arrange
            var videoEducation = new VideoEducation { Id = 1, Title = "Test Video" };
            var expectedResponse = new VideoEducationResponse { Id = 1, Title = "Test Video" };

            _mockRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<VideoEducation, bool>>>(), false, false, true, default))
                .ReturnsAsync(videoEducation);

            _mockMapper
                .Setup(mapper => mapper.Map<VideoEducationResponse>(videoEducation))
                .Returns(expectedResponse);

            // Act
            var result = await _service.GetAsync(x => x.Id == 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, result.Id);
            Assert.AreEqual(expectedResponse.Title, result.Title);
        }

        [Test]
        public async Task GetAsync_WhenVideoEducationDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<VideoEducation, bool>>>(), false, false, true, default))
                .ReturnsAsync((VideoEducation)null);

            // Act
            var result = await _service.GetAsync(x => x.Id == 1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddAsync_WhenCalled_AddsAndReturnsMappedResponse()
        {
            // Arrange
            var request = new VideoEducationCreateRequest { Title = "New Video" };
            var videoEducation = new VideoEducation { Id = 1, Title = "New Video" };
            var expectedResponse = new VideoEducationResponse { Id = 1, Title = "New Video" };

            _mockMapper
                .Setup(mapper => mapper.Map<VideoEducation>(request))
                .Returns(videoEducation);

            _mockRepository
                .Setup(repo => repo.AddAsync(videoEducation))
                .ReturnsAsync(videoEducation);

            _mockMapper
                .Setup(mapper => mapper.Map<VideoEducationResponse>(videoEducation))
                .Returns(expectedResponse);

            // Act
            var result = await _service.AddAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, result.Id);
            Assert.AreEqual(expectedResponse.Title, result.Title);
        }

        [Test]
        public async Task DeleteAsync_WhenIdExists_DeletesAndReturnsMappedResponse()
        {
            // Arrange
            int id = 1;
            var existingVideoEducation = new VideoEducation { Id = id, Title = "Test Video" };

            _mockRepository
                .Setup(repo => repo.GetAsync(x => x.Id == id, true, false, true, default))
                .ReturnsAsync(existingVideoEducation);

            _mockRepository
                .Setup(repo => repo.DeleteAsync(existingVideoEducation, It.IsAny<bool>()))
                .ReturnsAsync(existingVideoEducation);

            _mockMapper
                .Setup(mapper => mapper.Map<VideoEducationResponse>(existingVideoEducation))
                .Returns(new VideoEducationResponse { Id = id, Title = "Test Video" });

            _mockBusinessRules
                .Setup(x => x.VideoEducationIdShouldBeExistsWhenSelected(id))
                .Returns(Task.CompletedTask); // Eğer burayı unutursanız exception alınır.

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("Test Video", result.Title);

            _mockRepository.Verify(repo => repo.GetAsync(It.IsAny<Expression<Func<VideoEducation, bool>>>(), true, false, true, default), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteAsync(existingVideoEducation, It.IsAny<bool>()), Times.Once);
        }


        [Test]
        public async Task UpdateAsync_WhenIdExists_UpdatesAndReturnsMappedResponse()
        {
            // Arrange
            int id = 1;
            var existingVideoEducation = new VideoEducation { Id = id, Title = "Old Title" };
            var updateRequest = new VideoEducationUpdateRequest { Title = "New Title" };

            _mockRepository
                .Setup(repo => repo.GetAsync(x => x.Id == id, true, false, true, default))
                .ReturnsAsync(existingVideoEducation);

            _mockMapper
                .Setup(mapper => mapper.Map(updateRequest, existingVideoEducation))
                .Returns(existingVideoEducation);

            _mockRepository
                .Setup(repo => repo.UpdateAsync(existingVideoEducation))
                .ReturnsAsync(existingVideoEducation);

            _mockMapper
                .Setup(mapper => mapper.Map<VideoEducationResponse>(existingVideoEducation))
                .Returns(new VideoEducationResponse { Id = id, Title = "New Title" });

            _mockBusinessRules
                .Setup(x => x.VideoEducationIdShouldBeExistsWhenSelected(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(id, updateRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("New Title", result.Title);

            _mockRepository.Verify(repo => repo.GetAsync(x => x.Id == id, true, false, true, default), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(existingVideoEducation), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenVideoEducationDoesNotExist_ThrowsException()
        {
            // Arrange
            int id = 1;
            var updateRequest = new VideoEducationUpdateRequest { Title = "New Title" };

            _mockRepository
                .Setup(repo => repo.GetAsync(x => x.Id == id, true, false, true, default))
                .ReturnsAsync((VideoEducation)null);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(() => _service.UpdateAsync(id, updateRequest));
        }

        [Test]
        public async Task GetPaginateAsync_WhenNoVideoEducationsExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<VideoEducation, bool>>>(),null,true,false,true,default))
                .ReturnsAsync(new List<VideoEducation>());

            // Act
            var result = await _service.GetListAsync();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAsync_WhenEmptyCollection_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetListAsync(It.IsAny<Expression<Func<VideoEducation, bool>>>(),null,true,false,true,default))
                .ReturnsAsync(new List<VideoEducation>());

            // Act
            var result = await _service.GetListAsync();

            // Assert
            Assert.IsNull(result);
        }

    }
}
