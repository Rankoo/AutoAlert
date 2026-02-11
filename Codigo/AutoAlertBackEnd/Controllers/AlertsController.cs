using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly IAlertRepository _repo;

    public AlertsController(IAlertRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllAlerts")]
    public async Task<ActionResult<IEnumerable<Alerts>>> GetAll()
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

    [HttpGet("GetAlertById/{id}")]
    public async Task<ActionResult<Alerts>> Get(Guid id)
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

    [HttpGet("GetAlertsByServiceId/{serviceId}")]
    public async Task<ActionResult<IEnumerable<Alerts>>> GetByServiceId(Guid serviceId)
    {
        try
        {
            var list = await _repo.GetByServiceIdAsync(serviceId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetScheduledAlerts")]
    public async Task<ActionResult<IEnumerable<Alerts>>> GetScheduledAlerts([FromQuery] DateTime? fromDate = null)
    {
        try
        {
            var list = await _repo.GetScheduledAlertsAsync(fromDate);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateAlert")]
    public async Task<ActionResult<Alerts>> Create(Alerts alert)
    {
        try
        {
            var created = await _repo.CreateAsync(alert);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateAlert/{id}")]
    public async Task<IActionResult> Update(Guid id, Alerts alert)
    {
        try
        {
            if (id != alert.Id)
                return BadRequest();
            var updated = await _repo.UpdateAsync(alert);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteAlert/{id}")]
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

