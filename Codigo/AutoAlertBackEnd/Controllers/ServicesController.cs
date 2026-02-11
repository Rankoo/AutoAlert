using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _repo;

    public ServicesController(IServiceRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllServices")]
    public async Task<ActionResult<IEnumerable<Services>>> GetAll()
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

    [HttpGet("GetServiceById/{id}")]
    public async Task<ActionResult<Services>> Get(Guid id)
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

    [HttpGet("GetServicesByStoreId/{storeId}")]
    public async Task<ActionResult<IEnumerable<Services>>> GetByStoreId(Guid storeId)
    {
        try
        {
            var list = await _repo.GetByStoreIdAsync(storeId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateService")]
    public async Task<ActionResult<Services>> Create(Services service)
    {
        try
        {
            var created = await _repo.CreateAsync(service);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateService/{id}")]
    public async Task<IActionResult> Update(Guid id, Services service)
    {
        try
        {
            if (id != service.Id)
                return BadRequest();
            var updated = await _repo.UpdateAsync(service);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteService/{id}")]
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

