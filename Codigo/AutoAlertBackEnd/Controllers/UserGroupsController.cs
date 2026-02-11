using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserGroupsController : ControllerBase
{
    private readonly IUserGroupRepository _repo;

    public UserGroupsController(IUserGroupRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllUserGroups")]
    public async Task<ActionResult<IEnumerable<UserGroups>>> GetAll()
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

    [HttpGet("GetUserGroupById")]
    public async Task<ActionResult<UserGroups>> Get([FromQuery] Guid userId, [FromQuery] Guid groupId)
    {
        try
        {
            var item = await _repo.GetByIdAsync(userId, groupId);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetUserGroupsByUserId/{userId}")]
    public async Task<ActionResult<IEnumerable<UserGroups>>> GetByUserId(Guid userId)
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

    [HttpGet("GetUserGroupsByGroupId/{groupId}")]
    public async Task<ActionResult<IEnumerable<UserGroups>>> GetByGroupId(Guid groupId)
    {
        try
        {
            var list = await _repo.GetByGroupIdAsync(groupId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateUserGroup")]
    public async Task<ActionResult<UserGroups>> Create(UserGroups userGroup)
    {
        try
        {
            var created = await _repo.CreateAsync(userGroup);
            return CreatedAtAction(nameof(Get), new { userId = created.UserId, groupId = created.GroupId }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateUserGroup")]
    public async Task<IActionResult> Update([FromQuery] Guid userId, [FromQuery] Guid groupId, UserGroups userGroup)
    {
        try
        {
            if (userId != userGroup.UserId || groupId != userGroup.GroupId)
                return BadRequest();
            var updated = await _repo.UpdateAsync(userGroup);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteUserGroup")]
    public async Task<IActionResult> Delete([FromQuery] Guid userId, [FromQuery] Guid groupId)
    {
        try
        {
            var ok = await _repo.DeleteAsync(userId, groupId);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

