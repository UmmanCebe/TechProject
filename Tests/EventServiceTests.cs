using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Rules;

namespace TechCareer.Tests
{
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<EventBusinessRules> _mockBusinessRules;
        private EventService _eventService;

        [SetUp]
        public void SetUp()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<EventBusinessRules>(_mockEventRepository.Object);

            _eventService = new EventService(
                _mockEventRepository.Object,
                _mockBusinessRules.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task AddAsync_Should_Add_And_Return_Event()
        {
            // Arrange
            var createDto = new EventCreateRequestDto { };
          
            var @event = new Event { };

            var responseDto = new EventResponseDto { };


            _mockMapper.Setup(m => m.Map<Event>(createDto)).Returns(@event);
            _mockEventRepository.Setup(r => r.AddAsync(@event)).ReturnsAsync(@event);
            _mockMapper.Setup(m => m.Map<EventResponseDto>(@event)).Returns(responseDto);

            // Act
            var result = await _eventService.AddAsync(createDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDto, result);
            _mockEventRepository.Verify(r => r.AddAsync(@event), Times.Once);
        }
        [Test]
        public async Task DeleteAsync_Should_Delete_And_Return_Event()
        {
            // Arrange
            var id = Guid.NewGuid();
            var @event = new Event
            {
                Id = id,
                Title = "Test Event",
                Description = "Test Description"
            };

            var responseDto = new EventResponseDto
            {
                Id = id,
                Title = @event.Title,
                Description = @event.Description
            };

            _mockBusinessRules
                .Setup(businessRules => businessRules.EventIdShouldBeExistsWhenSelected(id))
                .Returns(Task.CompletedTask);

            _mockEventRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Event, bool>>>(), true, false, true, default))
                .ReturnsAsync(@event);

            _mockEventRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<Event>(), false))
                .ReturnsAsync(@event);

            _mockMapper
                .Setup(mapper => mapper.Map<EventResponseDto>(It.IsAny<Event>()))
                .Returns(responseDto);

            // Act
            var result = await _eventService.DeleteAsync(id, false);

            // Assert
            Assert.IsNotNull(result, "DeleteAsync metodu null döndürdü.");
            Assert.AreEqual(id, result.Id, "Id değerleri eşleşmiyor.");
            Assert.AreEqual("Test Event", result.Title, "Title değerleri eşleşmiyor.");
            Assert.AreEqual("Test Description", result.Description, "Description değerleri eşleşmiyor.");

            _mockEventRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Event>(), false), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<EventResponseDto>(It.IsAny<Event>()), Times.Once);
        }


        [Test]
        public async Task UpdateAsync_Should_Update_And_Return_Event()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new EventUpdateRequestDto
            {
                Title = "Updated Event",
                Description = "Updated Description"
            };

            var existingEvent = new Event { Id = id, Title = "Old Event", Description = "Old Description" };
            var updatedEvent = new Event { Id = id, Title = updateDto.Title, Description = updateDto.Description };

            var responseDto = new EventResponseDto
            {
                Id = id,
                Title = updateDto.Title,
                Description = updateDto.Description
            };

            _mockBusinessRules.Setup(r => r.EventIdShouldBeExistsWhenSelected(id)).Returns(Task.CompletedTask);
            _mockEventRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Event, bool>>>(), true, false, true, default))
                                 .ReturnsAsync(existingEvent);
            _mockMapper.Setup(m => m.Map(updateDto, existingEvent)).Returns(updatedEvent);
            _mockEventRepository.Setup(r => r.UpdateAsync(updatedEvent)).ReturnsAsync(updatedEvent);
            _mockMapper.Setup(m => m.Map<EventResponseDto>(updatedEvent)).Returns(responseDto);

            // Act
            var result = await _eventService.UpdateAsync(id, updateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDto, result);
             _mockEventRepository.Verify(r => r.UpdateAsync(updatedEvent), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_List_Of_Events()
        {
            // Arrange
            var eventList = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Event 1" },
                new Event { Id = Guid.NewGuid(), Title = "Event 2" }
            };

            var responseList = new List<EventResponseDto>
            {
                new EventResponseDto { Id = eventList[0].Id, Title = eventList[0].Title },
                new EventResponseDto { Id = eventList[1].Id, Title = eventList[1].Title }
            };

            _mockEventRepository.Setup(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(eventList);
            _mockMapper.Setup(m => m.Map<List<EventResponseDto>>(eventList)).Returns(responseList);

            // Act
            var result = await _eventService.GetListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseList.Count, result.Count);
            _mockEventRepository.Verify(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
