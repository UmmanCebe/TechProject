using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Concretes;

namespace TechCareer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{


    [HttpGet]
    public  async Task<IActionResult> GetList()
    {
        var result= await eventService.GetListAsync(include: true);
        return Ok(result);
    }


    [HttpGet("getallpaginate")]
    public async Task<IActionResult> GetPaginate([FromQuery] int index, [FromQuery] int size)
    {
        var result = await eventService.GetPaginateAsync(
            index: index,
            size: size,
            include: true);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result=await eventService.GetAsync(
            predicate: u => u.Id == id,
            include: true);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventCreateRequestDto request)
    {
        var result=await eventService.AddAsync(request);
        return Ok(result);
    }


    [HttpPut]
    public async Task<IActionResult> Update(Guid id , EventUpdateRequestDto request)
    {
        var result=await eventService.UpdateAsync(id, request);
        return Ok(result);
    }

    [ HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result=await eventService.DeleteAsync(id);
        return Ok(result);
    }

}
