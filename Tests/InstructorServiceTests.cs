namespace Tests
{
    using NUnit.Framework;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using TechCareer.DataAccess.Repositories.Abstracts;
    using TechCareer.Models.Dtos.Instructors.Request;
    using TechCareer.Models.Dtos.Instructors.Response;
    using TechCareer.Models.Entities;
    using TechCareer.Service.Concretes;
    using TechCareer.Service.Rules;

    [TestFixture]
    public class InstructorServiceTests
    {
        private Mock<IInstructorRepository> _mockInstructorRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<InstructorBusinessRules> _mockBusinessRules;
        private InstructorService _instructorService;

        [SetUp]
        public void SetUp()
        {
            _mockInstructorRepository = new Mock<IInstructorRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<InstructorBusinessRules>(_mockInstructorRepository.Object);

            _instructorService = new InstructorService(
                _mockInstructorRepository.Object,
                _mockMapper.Object,
                _mockBusinessRules.Object
            );
        }

        [Test]
        public async Task AddAsync_Should_Add_And_Return_Instructor()
        {
            // Arrange
            var createDto = new InstructorCreateRequestDto { /* DTO properties */ };
            var instructor = new Instructor { /* Instructor properties */ };
            var responseDto = new InstructorResponseDto { /* Response properties */ };

            _mockMapper.Setup(m => m.Map<Instructor>(createDto)).Returns(instructor);
            _mockInstructorRepository.Setup(r => r.AddAsync(instructor)).ReturnsAsync(instructor);
            _mockMapper.Setup(m => m.Map<InstructorResponseDto>(instructor)).Returns(responseDto);

            // Act
            var result = await _instructorService.AddAsync(createDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDto, result);
            _mockInstructorRepository.Verify(r => r.AddAsync(instructor), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Should_Delete_And_Return_Instructor()
        {
            // Arrange
            var id = Guid.NewGuid();
            var instructor = new Instructor { Id = id, Name = "Test Instructor" }; // Örnek veri
            var responseDto = new InstructorResponseDto { Id = id, Name = "Test Instructor" }; // Örnek yanýt DTO'su

            // Mocking Business Rules
            _mockBusinessRules.Setup(r => r.InstructorIdShouldBeExistsWhenSelected(id))
                              .Returns(Task.CompletedTask);

            // Mocking GetAsync method to return the instructor
            _mockInstructorRepository.Setup(r => r.GetAsync(It.Is<Expression<Func<Instructor, bool>>>(x => x.Compile()(instructor)), true, false, true, default))
                                      .ReturnsAsync(instructor);

            // Mocking DeleteAsync method to return the same instructor
            _mockInstructorRepository.Setup(r => r.DeleteAsync(instructor,false))
                                      .ReturnsAsync(instructor);

            // Mocking Map method to map the instructor to InstructorResponseDto
            _mockMapper.Setup(m => m.Map<InstructorResponseDto>(instructor))
                       .Returns(responseDto);

            // Act
            var result = await _instructorService.DeleteAsync(id,false);

            // Assert
            Assert.IsNotNull(result, "Sonuç null döndü.");
            Assert.That(result.Id, Is.EqualTo(responseDto.Id), "Id eþleþmiyor.");
            Assert.That(result.Name, Is.EqualTo(responseDto.Name), "Ýsim eþleþmiyor.");

            // Verifying method calls
            _mockBusinessRules.Verify(r => r.InstructorIdShouldBeExistsWhenSelected(id), Times.Once);
            _mockInstructorRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Instructor, bool>>>(),true,false,true,default), Times.Once);
            _mockInstructorRepository.Verify(r => r.DeleteAsync(instructor,false), Times.Once);
        }


        [Test]
        public async Task GetAllAsync_Should_Return_List_Of_Instructors()
        {
            // Arrange
            var instructorList = new List<Instructor>
        {
            new Instructor { Id = Guid.NewGuid() },
            new Instructor { Id = Guid.NewGuid() }
        };

            var responseDtoList = new List<InstructorResponseDto>
        {
            new InstructorResponseDto { /* Response properties */ },
            new InstructorResponseDto { /* Response properties */ }
        };

            _mockInstructorRepository.Setup(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()))
                .ReturnsAsync(instructorList);
            _mockMapper.Setup(m => m.Map<List<InstructorResponseDto>>(instructorList)).Returns(responseDtoList);

            // Act
            var result = await _instructorService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDtoList.Count, result.Count);
            _mockInstructorRepository.Verify(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task UpdateAsync_Should_Update_And_Return_Instructor()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new InstructorUpdateRequestDto { Name = "deneme", About = "deneme" };
            var existingInstructor = new Instructor { Id = id };  // Bu nesne doðru þekilde baþlatýlýyor
            var updatedInstructor = new Instructor { Id = id };
            var responseDto = new InstructorResponseDto { Id = id, Name = "deneme", About = "deneme" };

            // Mocking Business Rules
            _mockBusinessRules.Setup(r => r.InstructorIdShouldBeExistsWhenSelected(id))
                              .Returns(Task.CompletedTask);

            // Mocking GetAsync method, return existing instructor
            _mockInstructorRepository.Setup(r => r.GetAsync(It.Is<Expression<Func<Instructor, bool>>>(x => x.Compile()(existingInstructor)), true, false, true, default))
                                     .ReturnsAsync(existingInstructor); // Doðru þekilde mock yapýlýyor

            // Mocking Map method for Update
            _mockMapper.Setup(m => m.Map(updateDto, existingInstructor))
                       .Returns(updatedInstructor);

            // Mocking UpdateAsync method
            _mockInstructorRepository.Setup(r => r.UpdateAsync(updatedInstructor))
                                     .ReturnsAsync(updatedInstructor);

            // Mocking Map method for Response DTO
            _mockMapper.Setup(m => m.Map<InstructorResponseDto>(updatedInstructor))
                       .Returns(responseDto);

            // Act
            var result = await _instructorService.UpdateAsync(updateDto, id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDto, result);

            // Verifying method calls
            _mockBusinessRules.Verify(r => r.InstructorIdShouldBeExistsWhenSelected(id), Times.Once);
            _mockInstructorRepository.Verify(r => r.UpdateAsync(updatedInstructor), Times.Once);
        }

    }
}