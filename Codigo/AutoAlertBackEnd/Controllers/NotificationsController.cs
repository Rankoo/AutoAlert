using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepository _repo;

    public NotificationsController(INotificationRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllNotifications")]
    public async Task<ActionResult<IEnumerable<Notifications>>> GetAll()
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

    [HttpGet("GetNotificationById/{id}")]
    public async Task<ActionResult<Notifications>> Get(Guid id)
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

    [HttpGet("GetNotificationsByAlertId/{alertId}")]
    public async Task<ActionResult<IEnumerable<Notifications>>> GetByAlertId(Guid alertId)
    {
        try
        {
            var list = await _repo.GetByAlertIdAsync(alertId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetNotificationsByUserId/{userId}")]
    public async Task<ActionResult<IEnumerable<Notifications>>> GetByUserId(Guid userId)
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

    [HttpPost("CreateNotification")]
    public async Task<ActionResult<Notifications>> Create(Notifications notification)
    {
        try
        {
            var created = await _repo.CreateAsync(notification);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateNotification/{id}")]
    public async Task<IActionResult> Update(Guid id, Notifications notification)
    {
        try
        {
            if (id != notification.Id)
                return BadRequest();
            var updated = await _repo.UpdateAsync(notification);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteNotification/{id}")]
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

