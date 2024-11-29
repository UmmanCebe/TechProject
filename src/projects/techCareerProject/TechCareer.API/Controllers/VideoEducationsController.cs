using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoEducationsController(IVideoEducationService videoEducationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await videoEducationService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("getallpaginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] int index, [FromQuery] int size)
        {
            var result = await videoEducationService.GetPaginateAsync(index: index, size: size);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await videoEducationService.GetAsync(u => u.Id == id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoEducationCreateRequest request)
        {
            var result = await videoEducationService.AddAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, VideoEducationUpdateRequest request)
        {
            var result = await videoEducationService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await videoEducationService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
