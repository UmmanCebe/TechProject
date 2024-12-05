using Core.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using TechCareer.API.Controllers;
using TechCareer.Models.Dtos.Categories.RequestDto;
using TechCareer.Models.Dtos.Categories.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;

namespace Tests;

[TestFixture]
public class CategoryControllerTests
{

    private Mock<ICategoryService> _mockCategoryService;
    private CategoryController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _controller = new CategoryController(_mockCategoryService.Object);
    }

    [Test]
    public async Task AddCategory_Should_Return_CreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateCategoryRequestDto("Electronics");
        var responseDto = new CategoryDto(1, "Electronics", DateTime.UtcNow, DateTime.UtcNow);
        _mockCategoryService.Setup(s => s.AddAsync(createDto)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.AddCategory(createDto, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        var createdResult = (CreatedAtActionResult)result;
        Assert.AreEqual(nameof(_controller.GetCategory), createdResult.ActionName);
        Assert.AreEqual(responseDto, createdResult.Value);
        _mockCategoryService.Verify(s => s.AddAsync(createDto), Times.Once);
    }

    [Test]
    public async Task GetCategory_Should_Return_OkResult_With_Category()
    {
        // Arrange
        var id = 1;
        var responseDto = new CategoryDto(id, "Electronics", DateTime.UtcNow, DateTime.UtcNow);
        _mockCategoryService
     .Setup(s => s.GetAsync(
         c => c.Id == id,
         It.IsAny<bool>(),      // include
         It.IsAny<bool>(),      // withDeleted
         It.IsAny<bool>(),      // enableTracking
         It.IsAny<CancellationToken>()  // cancellationToken
     ))
     .ReturnsAsync(responseDto);

        // Act
        var result = await _controller.GetCategory(id, CancellationToken.None);

        // Assert
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result); // Tür kontrolü
        var okResult = result as OkObjectResult;      // Tür dönüşümü
        Assert.NotNull(okResult);                     // Null olmadığını kontrol et
        Assert.AreEqual(responseDto, okResult.Value);
    }

    [Test]
    public async Task GetCategory_Should_Return_NotFound_When_Category_Not_Found()
    {
        // Arrange
        var id = 1;
        _mockCategoryService.Setup(s => s.GetAsync(c => c.Id == id, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync((CategoryDto)null);

        // Act
        var result = await _controller.GetCategory(id, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        _mockCategoryService.Verify(
            s => s.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Test]
    public async Task UpdateCategory_Should_Return_OkResult_With_Updated_Category()
    {
        // Arrange
        var id = 1;
        var updateDto = new UpdateCategoryRequestDto("Home Appliances");
        var responseDto = new CategoryDto(id, "Home Appliances", DateTime.UtcNow, DateTime.UtcNow);
        _mockCategoryService.Setup(s => s.UpdateAsync(id, updateDto)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.UpdateCategory(id, updateDto, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        // Tür dönüşümünü gerçekleştirin
        var okResult = result as OkObjectResult;

        // Null olup olmadığını kontrol edin (opsiyonel)
        Assert.NotNull(okResult);
        _mockCategoryService.Verify(s => s.UpdateAsync(id, updateDto), Times.Once);
    }

    [Test]
    public async Task DeleteCategory_Should_Return_OkResult_With_Deleted_Category()
    {
        // Arrange
        var id = 1;
        var responseDto = new CategoryDto(id, "Electronics", DateTime.UtcNow, DateTime.UtcNow);
        _mockCategoryService.Setup(s => s.DeleteAsync(id, false)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.DeleteCategory(id, false, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        // Safely cast result to OkObjectResult
        var okResult = result as OkObjectResult;

        // Optionally check if casting succeeded
        Assert.NotNull(okResult);
        Assert.AreEqual(responseDto, ((OkObjectResult)okResult).Value);
        _mockCategoryService.Verify(s => s.DeleteAsync(id, false), Times.Once);
    }

    [Test]
    public async Task GetAllPaginate_Should_Return_OkResult_With_Paginated_Categories()
    {
        // Arrange
        var paginatedResult = new Paginate<CategoryDto>
        {
            Items = new List<CategoryDto>
            {
                new CategoryDto(1, "Electronics", DateTime.UtcNow, DateTime.UtcNow),
                new CategoryDto(2, "Books", DateTime.UtcNow, DateTime.UtcNow)
            },
            Index = 0,
            Size = 2,
            Count = 2
        };

        _mockCategoryService.Setup(s => s.GetPaginateAsync(It.IsAny<Expression<Func<Category, bool>>>(), null, false, 0, 2, false, true, It.IsAny<CancellationToken>()))
        .ReturnsAsync(paginatedResult);

        // Act
        var result = await _controller.GetAllPaginate(0, 2, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        // Safely cast result to OkObjectResult
        var okResult = result as OkObjectResult;

        // Optionally, assert that the cast was successful
        Assert.NotNull(okResult);
        Assert.AreEqual(paginatedResult, ((OkObjectResult)okResult).Value);
        _mockCategoryService.Verify(s => s.GetPaginateAsync(It.IsAny<Expression<Func<Category, bool>>>(), null, false, 0, 2, false, true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetList_Should_Return_OkResult_With_Categories()
    {
        // Arrange
        var categoryList = new List<CategoryDto>
        {
            new CategoryDto(1, "Electronics", DateTime.UtcNow, DateTime.UtcNow),
            new CategoryDto(2, "Books", DateTime.UtcNow, DateTime.UtcNow)
        };

        _mockCategoryService.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>(), null, false, false, true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoryList);

        // Act
        var result = await _controller.GetList(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);

        // Safely cast result to OkObjectResult
        var okResult = result as OkObjectResult;

        // Ensure the cast was successful (okResult is not null)
        Assert.NotNull(okResult);
        Assert.AreEqual(categoryList, ((OkObjectResult)okResult).Value);
        _mockCategoryService.Verify(s => s.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>(), null, false, false, true, It.IsAny<CancellationToken>()), Times.Once);
    }








}
