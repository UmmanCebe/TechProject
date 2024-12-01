using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Categories.RequestDto;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService _categoryService): ControllerBase
{

    [HttpPost("add")]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddAsync(dto);
        return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAsync(c => c.Id == id, cancellationToken: cancellationToken);
        if (result == null)
            return NotFound($"Kategori {id} bulunamadı.");
        return Ok(result);
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id, [FromQuery] bool permanent = false, CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.DeleteAsync(id, permanent);
        return Ok(result);
    }

    [HttpGet("getallpaginate")]
    public async Task<IActionResult> GetAllPaginate([FromQuery] int index = 0, [FromQuery] int size = 10, CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.GetPaginateAsync(index: index, size: size, cancellationToken: cancellationToken);
        return Ok(result);
    }

    [HttpGet("getlist")]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.GetListAsync(cancellationToken: cancellationToken);
        return Ok(result);
    }


















}
