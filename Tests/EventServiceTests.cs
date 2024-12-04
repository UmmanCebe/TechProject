using System.Linq.Expressions;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Rules;

namespace Tests
{
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _mockRepository;
        private Mock<EventBusinessRules> _mockBusinessRules;
        private Mock<IMapper> _mockMapper;
        private EventService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IEventRepository>();
            _mockBusinessRules = new Mock<EventBusinessRules>(_mockRepository.Object);
            _mockMapper = new Mock<IMapper>();
            _service = new EventService(_mockRepository.Object, _mockBusinessRules.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAsync_WhenEventExists_ReturnsMappedResponse()
        {
            // Arrange
            var @event = new Event { Id = Guid.NewGuid(), Title = "Test Event" };
            var expectedResponse = new EventResponseDto { Id = @event.Id, Title = "Test Event" };

            _mockRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Event, bool>>>(), false, false, true, default))
                .ReturnsAsync(@event);

            _mockMapper
                .Setup(mapper => mapper.Map<EventResponseDto>(@event))
                .Returns(expectedResponse);

            // Act
            var result = await _service.GetAsync(x => x.Id == @event.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, result.Id);
            Assert.AreEqual(expectedResponse.Title, result.Title);
        }

        [Test]
        public async Task GetAsync_WhenEventDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Event, bool>>>(), false, false, true, default))
                .ReturnsAsync((Event)null);

            // Act
            var result = await _service.GetAsync(x => x.Id == Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddAsync_WhenCalled_AddsAndReturnsMappedResponse()
        {
            // Arrange
            var request = new EventCreateRequestDto { Title = "New Event" };
            var @event = new Event { Id = Guid.NewGuid(), Title = "New Event" };
            var expectedResponse = new EventResponseDto { Id = @event.Id, Title = "New Event" };

            _mockMapper
                .Setup(mapper => mapper.Map<Event>(request))
                .Returns(@event);

            _mockRepository
                .Setup(repo => repo.AddAsync(@event))
                .ReturnsAsync(@event);

            _mockMapper
                .Setup(mapper => mapper.Map<EventResponseDto>(@event))
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
            var id = Guid.NewGuid();
            var existingEvent = new Event { Id = id, Title = "Test Event" };

            _mockRepository
                .Setup(repo => repo.GetAsync(x => x.Id == id, true, false, true, default))
                .ReturnsAsync(existingEvent);

            _mockRepository
                .Setup(repo => repo.DeleteAsync(existingEvent, It.IsAny<bool>()))
                .ReturnsAsync(existingEvent);

            _mockMapper
                .Setup(mapper => mapper.Map<EventResponseDto>(existingEvent))
                .Returns(new EventResponseDto { Id = id, Title = "Test Event" });

            _mockBusinessRules
                .Setup(x => x.EventIdShouldBeExistsWhenSelected(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("Test Event", result.Title);

            _mockRepository.Verify(repo => repo.GetAsync(It.IsAny<Expression<Func<Event, bool>>>(), true, false, true, default), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteAsync(existingEvent, It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenIdExists_UpdatesAndReturnsMappedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingEvent = new Event { Id = id, Title = "Old Title" };
            var updateRequest = new EventUpdateRequestDto { Title = "New Title" };

            _mockRepository
                .Setup(repo => repo.GetAsync(x => x.Id == id, true, false, true, default))
                .ReturnsAsync(existingEvent);

            _mockMapper
                .Setup(mapper => mapper.Map(updateRequest, existingEvent))
                .Returns(existingEvent);

            _mockRepository
                .Setup(repo => repo.UpdateAsync(existingEvent))
                .ReturnsAsync(existingEvent);

            _mockMapper
                .Setup(mapper => mapper.Map<EventResponseDto>(existingEvent))
                .Returns(new EventResponseDto { Id = id, Title = "New Title" });

            _mockBusinessRules
                .Setup(x => x.EventIdShouldBeExistsWhenSelected(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(id, updateRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("New Title", result.Title);

            _mockRepository.Verify(repo => repo.GetAsync(x => x.Id == id, true, false, true, default), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(existingEvent), Times.Once);
        }
    }
}
