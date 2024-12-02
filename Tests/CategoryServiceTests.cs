
using AutoMapper;
using Core.Persistence.Extensions;
using Moq;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Categories.RequestDto;
using TechCareer.Models.Dtos.Categories.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Rules;

namespace Tests;

[TestFixture]
public class CategoryServiceTests
{


    private Mock<ICategoryRepository> _mockCategoryRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<CategoryBusinessRules> _mockBusinessRules;
    private CategoryService _categoryService;

    [SetUp]
    public void SetUp()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockMapper = new Mock<IMapper>();

        // CategoryBusinessRules yalnızca ICategoryRepository aldığı için doğru şekilde mock'lanıyor:
        _mockBusinessRules = new Mock<CategoryBusinessRules>(_mockCategoryRepository.Object);

        // CategoryService, business rules ve diğer bağımlılıklarla oluşturuluyor:
        _categoryService = new CategoryService(
            _mockCategoryRepository.Object,
            
            _mockBusinessRules.Object,
            _mockMapper.Object
        );
    }

    [Test]
    public async Task AddAsync_Should_Add_And_Return_Category()
    {
        // Arrange
        var createDto = new CreateCategoryRequestDto("Electronics");
        var category = new Category { Id = 1, Name = "Electronics" };
        var responseDto = new CategoryDto(1, "Electronics", DateTime.UtcNow, DateTime.UtcNow);

        _mockMapper.Setup(m => m.Map<Category>(createDto)).Returns(category);
        _mockCategoryRepository.Setup(r => r.AddAsync(category)).ReturnsAsync(category);
        _mockMapper.Setup(m => m.Map<CategoryDto>(category)).Returns(responseDto);

        // Act
        var result = await _categoryService.AddAsync(createDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(responseDto, result);
        _mockCategoryRepository.Verify(r => r.AddAsync(category), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_Should_Return_List_Of_Categories()
    {
        // Arrange
        var categoryList = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" }
            };

        var responseDtoList = new List<CategoryDto>
            {
                new CategoryDto(1, "Electronics", DateTime.UtcNow, DateTime.UtcNow),
                new CategoryDto(2, "Books", DateTime.UtcNow, DateTime.UtcNow)
            };

        _mockCategoryRepository.Setup(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoryList);
        _mockMapper.Setup(m => m.Map<List<CategoryDto>>(categoryList)).Returns(responseDtoList);

        // Act
        var result = await _categoryService.GetListAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(responseDtoList.Count, result.Count);
        _mockCategoryRepository.Verify(r => r.GetListAsync(null, null, false, false, true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_Should_Delete_And_Return_Category()
    {
        // Arrange
        var id = 1;
        var category = new Category { Id = id, Name = "Electronics" };
        var responseDto = new CategoryDto(id, "Electronics", DateTime.UtcNow, DateTime.UtcNow);

        _mockBusinessRules.Setup(r => r.CategoryIdShouldBeExistsWhenSelected(id))
                          .Returns(Task.CompletedTask);

        _mockCategoryRepository.Setup(r => r.GetAsync(c => c.Id == id, true, false, true, default))
                                .ReturnsAsync(category);

        _mockCategoryRepository.Setup(r => r.DeleteAsync(category, false))
                                .ReturnsAsync(category);

        _mockMapper.Setup(m => m.Map<CategoryDto>(category))
                   .Returns(responseDto);

        // Act
        var result = await _categoryService.DeleteAsync(id, false);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(responseDto, result);
        _mockCategoryRepository.Verify(r => r.DeleteAsync(category, false), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_Should_Update_And_Return_Category()
    {
        // Arrange
        var id = 1;
        var updateDto = new UpdateCategoryRequestDto("Home Appliances");
        var existingCategory = new Category { Id = id, Name = "Electronics" };
        var updatedCategory = new Category { Id = id, Name = "Home Appliances" };
        var responseDto = new CategoryDto(id, "Home Appliances", DateTime.UtcNow, DateTime.UtcNow);

        _mockBusinessRules.Setup(r => r.CategoryIdShouldBeExistsWhenSelected(id))
                          .Returns(Task.CompletedTask);

        _mockCategoryRepository.Setup(r => r.GetAsync(c => c.Id == id, true, false, true, default))
                                .ReturnsAsync(existingCategory);

        _mockMapper.Setup(m => m.Map(updateDto, existingCategory))
                   .Returns(updatedCategory);

        _mockCategoryRepository.Setup(r => r.UpdateAsync(updatedCategory))
                                .ReturnsAsync(updatedCategory);

        _mockMapper.Setup(m => m.Map<CategoryDto>(updatedCategory))
                   .Returns(responseDto);

        // Act
        var result = await _categoryService.UpdateAsync(id, updateDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(responseDto, result);
        _mockCategoryRepository.Verify(r => r.UpdateAsync(updatedCategory), Times.Once);
    }



}








