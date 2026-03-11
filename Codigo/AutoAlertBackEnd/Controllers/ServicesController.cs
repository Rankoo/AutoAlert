using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _repo;

    public ServicesController(IServiceRepository repo)
    {
        _repo = repo;
    }

    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet]
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

    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("{id}")]
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

    [Authorize(Policy = "CREATE_SERVICES")]
    [HttpPost]
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

    [Authorize(Policy = "EDIT_SERVICES")]
    [HttpPut("{id}")]
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

    [Authorize(Policy = "DELETE_SERVICES")]
    [HttpDelete("{id}")]
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
