using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserSubmodulesController : ControllerBase
{
    private readonly IUserSubmoduleRepository _repo;

    public UserSubmodulesController(IUserSubmoduleRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllUserSubmodules")]
    public async Task<ActionResult<IEnumerable<UserSubmodules>>> GetAll()
    {
        try
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetUserSubmoduleById")]
    public async Task<ActionResult<UserSubmodules>> Get([FromQuery] Guid userId, [FromQuery] Guid subModuleId)
    {
        try
        {
            var item = await _repo.GetByIdAsync(userId, subModuleId);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetUserSubmodulesByUserId/{userId}")]
    public async Task<ActionResult<IEnumerable<UserSubmodules>>> GetByUserId(Guid userId)
    {
        try
        {
            var list = await _repo.GetByUserIdAsync(userId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetUserSubmodulesBySubModuleId/{subModuleId}")]
    public async Task<ActionResult<IEnumerable<UserSubmodules>>> GetBySubModuleId(Guid subModuleId)
    {
        try
        {
            var list = await _repo.GetBySubModuleIdAsync(subModuleId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateUserSubmodule")]
    public async Task<ActionResult<UserSubmodules>> Create(UserSubmodules userSubmodule)
    {
        try
        {
            var created = await _repo.CreateAsync(userSubmodule);
            return CreatedAtAction(nameof(Get), new { userId = created.UserId, subModuleId = created.SubModuleId }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateUserSubmodule")]
    public async Task<IActionResult> Update([FromQuery] Guid userId, [FromQuery] Guid subModuleId, UserSubmodules userSubmodule)
    {
        try
        {
            if (userId != userSubmodule.UserId || subModuleId != userSubmodule.SubModuleId)
                return BadRequest();
            var updated = await _repo.UpdateAsync(userSubmodule);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteUserSubmodule")]
    public async Task<IActionResult> Delete([FromQuery] Guid userId, [FromQuery] Guid subModuleId)
    {
        try
        {
            var ok = await _repo.DeleteAsync(userId, subModuleId);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

