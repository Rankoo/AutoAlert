using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/stores")]
public class StoresController : ControllerBase
{
    private readonly IStoreRepository _repo;

    public StoresController(IStoreRepository repo)
    {
        _repo = repo;
    }

    [Authorize(Policy = "VIEW_STORES")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Stores>>> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list);
    }

    [Authorize(Policy = "VIEW_STORES")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Stores>> Get(Guid id)
    {
        try {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) 
                return NotFound();
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "CREATE_STORES")]
    [HttpPost]
    public async Task<ActionResult<Stores>> Create(Stores store)
    {
        try {
            var created = await _repo.CreateAsync(store);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "EDIT_STORES")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Stores store)
    {
        try {
            if (id != store.Id) return BadRequest();
            var updated = await _repo.UpdateAsync(store);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "DELETE_STORES")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try {
            var ok = await _repo.DeleteAsync(id);
            if (!ok)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
