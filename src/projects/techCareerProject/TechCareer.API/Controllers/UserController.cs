using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Concretes;

namespace TechCareer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(
     int id,
     bool include = false,
     bool withDeleted = false,
     bool enableTracking = true,
     CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetAsync(
            (Expression<Func<User, bool>>)(u => u.Id == id), // Expression türünü açıkça belirtiyoruz
            include,
            withDeleted,
            enableTracking,
            cancellationToken);

        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }


    [HttpGet("paginate")]
    public async Task<IActionResult> GetPaginateAsync(
        int index = 0,
        int size = 10,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var paginatedUsers = await _userService.GetPaginateAsync(null, null, include, index, size, withDeleted, enableTracking, cancellationToken);
        return Ok(paginatedUsers);
    }


    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync(
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetListAsync(null, null, include, withDeleted, enableTracking, cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdUser = await _userService.AddAsync(user);
        return CreatedAtAction(nameof(GetAsync), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] User user)
    {
        if (id != user.Id)
            return BadRequest("ID mismatch.");

        var updatedUser = await _userService.UpdateAsync(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, bool permanent = false)
    {
        var user = await _userService.GetAsync(u => u.Id == id);
        if (user == null)
            return NotFound("User not found.");

        var deletedUser = await _userService.DeleteAsync(user, permanent);
        return Ok(deletedUser);
    }
}