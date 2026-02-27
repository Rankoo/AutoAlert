using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoleSubModulesController : ControllerBase
{
    private readonly IRoleSubModuleRepository _repo;

    public RoleSubModulesController(IRoleSubModuleRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllRoleSubModules")]
    public async Task<ActionResult<IEnumerable<RoleSubModules>>> GetAll()
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

    [HttpGet("GetRoleSubModuleById/{id}")]
    public async Task<ActionResult<RoleSubModules>> Get(Guid id)
    {
        try
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetRoleSubModulesByRoleId/{roleId}")]
    public async Task<ActionResult<IEnumerable<RoleSubModules>>> GetByRoleId(Guid roleId)
    {
        try
        {
            var list = await _repo.GetByRoleIdAsync(roleId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetRoleSubModulesBySubModuleId/{subModuleId}")]
    public async Task<ActionResult<IEnumerable<RoleSubModules>>> GetBySubModuleId(Guid subModuleId)
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

    [HttpPost("CreateRoleSubModule")]
    public async Task<ActionResult<RoleSubModules>> Create(RoleSubModules roleSubModule)
    {
        try
        {
            var created = await _repo.CreateAsync(roleSubModule);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateRoleSubModule/{id}")]
    public async Task<IActionResult> Update(Guid id, RoleSubModules roleSubModule)
    {
        try
        {
            if (id != roleSubModule.Id)
                return BadRequest();
            var updated = await _repo.UpdateAsync(roleSubModule);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteRoleSubModule/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var ok = await _repo.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

