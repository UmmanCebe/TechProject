using Core.CrossCuttingConcerns.DtoBases;
using Core.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechCareer.API.Controllers;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Service.Abstracts;

namespace Tests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private Mock<IEventService> _mockEventService;
        private EventsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockEventService = new Mock<IEventService>();
            _controller = new EventsController(_mockEventService.Object);
        }

        [Test]
        public async Task GetList_ShouldReturnOkResultWithEvents()
        {
            // Arrange
            var events = new List<EventResponseDto>
    {
        new EventResponseDto { Id = Guid.NewGuid(), Title = "Event 1" },
        new EventResponseDto { Id = Guid.NewGuid(), Title = "Event 2" }
    };

            _mockEventService
                .Setup(service => service.GetListAsync(null, null, false, false, true, default))
                .ReturnsAsync(events);

            // Act
            var result = await _controller.GetList();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(events, okResult.Value);
        }


        [Test]
        public async Task Get_ShouldReturnOkResultWithEvent_WhenEventExists()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventDto = new EventResponseDto { Id = eventId, Title = "Event 1" };
            _mockEventService.Setup(service => service.GetAsync(e => e.Id == eventId, false, false, true, default))
                .ReturnsAsync(eventDto);

            // Act
            var result = await _controller.Get(eventId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(eventDto, okResult.Value);
        }

        [Test]
        public async Task Add_ShouldReturnOkResultWithCreatedEvent()
        {
            // Arrange
            var dto = new EventCreateRequestDto { Title = "New Event" };
            var createdEvent = new EventResponseDto { Id = Guid.NewGuid(), Title = "New Event" };
            _mockEventService.Setup(service => service.AddAsync(dto))
                .ReturnsAsync(createdEvent);

            // Act
            var result = await _controller.Add(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(createdEvent, okResult.Value);
        }

        [Test]
        public async Task Update_ShouldReturnOkResultWithUpdatedEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var dto = new EventUpdateRequestDto { Title = "Updated Event" };
            var updatedEvent = new EventResponseDto { Id = eventId, Title = "Updated Event" };
            _mockEventService.Setup(service => service.UpdateAsync(eventId, dto))
                .ReturnsAsync(updatedEvent);

            // Act
            var result = await _controller.Update(eventId, dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedEvent, okResult.Value);
        }

        [Test]
        public async Task Delete_ShouldReturnOkResultWithDeletedEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var deletedEvent = new EventResponseDto { Id = eventId, Title = "Deleted Event" };
            _mockEventService.Setup(service => service.DeleteAsync(eventId, false))
                .ReturnsAsync(deletedEvent);

            // Act
            var result = await _controller.Delete(eventId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(deletedEvent, okResult.Value);
        }

        [Test]
        public async Task GetPaginate_ShouldReturnOkResultWithPaginatedEvents()
        {
            // Arrange
            var paginatedEvents = new Paginate<EventResponseDto>
            {
                Items = new List<EventResponseDto>
                {
                    new EventResponseDto { Id = Guid.NewGuid(), Title = "Event 1" },
                    new EventResponseDto { Id = Guid.NewGuid(), Title = "Event 2" }
                },
            };
            _mockEventService.Setup(service => service.GetPaginateAsync(null, null, false, 0, 10, false, true, default))
                .ReturnsAsync(paginatedEvents);

            // Act
            var result = await _controller.GetPaginate(0, 10);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(paginatedEvents, okResult.Value);
        }
    }
}
