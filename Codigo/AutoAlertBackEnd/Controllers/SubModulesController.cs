using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubModulesController : ControllerBase
{
    private readonly ISubModuleRepository _repo;

    public SubModulesController(ISubModuleRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllSubModules")]
    public async Task<ActionResult<IEnumerable<SubModules>>> GetAll()
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

    [HttpGet("GetSubModuleById/{id}")]
    public async Task<ActionResult<SubModules>> Get(Guid id)
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

    [HttpGet("GetSubModulesByModuleId/{moduleId}")]
    public async Task<ActionResult<IEnumerable<SubModules>>> GetByModuleId(Guid moduleId)
    {
        try
        {
            var list = await _repo.GetByModuleIdAsync(moduleId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateSubModule")]
    public async Task<ActionResult<SubModules>> Create(SubModules subModule)
    {
        try
        {
            var created = await _repo.CreateAsync(subModule);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateSubModule/{id}")]
    public async Task<IActionResult> Update(Guid id, SubModules subModule)
    {
        try
        {
            if (id != subModule.Id)
                return BadRequest();
            var updated = await _repo.UpdateAsync(subModule);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteSubModule/{id}")]
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

