using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly ILogRepository _repo;

    public LogsController(ILogRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllLogs")]
    public async Task<ActionResult<IEnumerable<Logs>>> GetAll()
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

    [HttpGet("GetLogById/{id}")]
    public async Task<ActionResult<Logs>> Get(Guid id)
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

    [HttpGet("GetLogsByUserId/{userId}")]
    public async Task<ActionResult<IEnumerable<Logs>>> GetByUserId(Guid userId)
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

    [HttpGet("GetLogsByTableName/{tableName}")]
    public async Task<ActionResult<IEnumerable<Logs>>> GetByTableName(string tableName)
    {
        try
        {
            var list = await _repo.GetByTableNameAsync(tableName);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetLogsByDateRange")]
    public async Task<ActionResult<IEnumerable<Logs>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var list = await _repo.GetByDateRangeAsync(startDate, endDate);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateLog")]
    public async Task<ActionResult<Logs>> Create(Logs log)
    {
        try
        {
            var created = await _repo.CreateAsync(log);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteLog/{id}")]
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

