using AutoAlertBackEnd.Dtos;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/services")]
public class ServicesController(IServiceRepository repo) : ControllerBase
{
    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("catalogs")]
    public async Task<ActionResult<ServiceCatalogsDto>> GetCatalogs() => Ok(await repo.GetCatalogsAsync());

    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("quantities")]
    public async Task<ActionResult<ServiceQuantitiesDto>> GetQuantities() => Ok(await repo.GetQuantitiesAsync());

    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet]
    public async Task<ActionResult<PagedServicesDto>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] Guid? storeId = null)
    {
        if (page < 1 || pageSize is < 1 or > 100) return BadRequest("page debe ser mayor que 0 y pageSize debe estar entre 1 y 100.");
        return Ok(await repo.GetAllAsync(page, pageSize, search, storeId));
    }

    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> Get(Guid id)
    {
        var item = await repo.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [Authorize(Policy = "CREATE_SERVICES")]
    [HttpPost]
    public async Task<ActionResult<ServiceDto>> Create(CreateServiceDto service)
    {
        try { var created = await repo.CreateAsync(service); return CreatedAtAction(nameof(Get), new { id = created.Id }, created); }
        catch (InvalidOperationException e) { return BadRequest(e.Message); }
    }

    [Authorize(Policy = "EDIT_SERVICES")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateServiceDto service)
    {
        try { return await repo.UpdateAsync(id, service) is null ? NotFound() : NoContent(); }
        catch (InvalidOperationException e) { return BadRequest(e.Message); }
    }

    [Authorize(Policy = "DELETE_SERVICES")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { return await repo.DeleteAsync(id) ? NoContent() : NotFound(); }
        catch (InvalidOperationException e) { return BadRequest(e.Message); }
    }
}
