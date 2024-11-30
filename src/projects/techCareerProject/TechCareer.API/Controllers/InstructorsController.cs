using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController(IInstructorService instructorService) : ControllerBase
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await instructorService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("getone/{id:guid}")]
        public async Task<IActionResult> GetOne([FromRoute(Name = "id")] Guid id)
        {
            var result = await instructorService.GetOneAsync(i => i.Id == id);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InstructorCreateRequestDto dto)
        {
            var result = await instructorService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute(Name = "id")] Guid id, [FromBody] InstructorUpdateRequestDto dto)
        {
            var result = await instructorService.UpdateAsync(dto, id);
            return Ok(result);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid id)
        {
            var result = await instructorService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("getallpaginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] int index, [FromQuery] int size)
        {
            var result = await instructorService.GetPaginateAsync(index: index, size: size);
            return Ok(result);
        }
    }
}