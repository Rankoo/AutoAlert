using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserCompaniesController : ControllerBase
{
    private readonly IUserCompanyRepository _repo;

    public UserCompaniesController(IUserCompanyRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAllUserCompanies")]
    public async Task<ActionResult<IEnumerable<UserCompanies>>> GetAll()
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

    [HttpGet("GetUserCompanyById")]
    public async Task<ActionResult<UserCompanies>> Get([FromQuery] Guid userId, [FromQuery] Guid companyId)
    {
        try
        {
            var item = await _repo.GetByIdAsync(userId, companyId);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("GetUserCompaniesByUserId/{userId}")]
    public async Task<ActionResult<IEnumerable<UserCompanies>>> GetByUserId(Guid userId)
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

    [HttpGet("GetUserCompaniesByCompanyId/{companyId}")]
    public async Task<ActionResult<IEnumerable<UserCompanies>>> GetByCompanyId(Guid companyId)
    {
        try
        {
            var list = await _repo.GetByCompanyIdAsync(companyId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("CreateUserCompany")]
    public async Task<ActionResult<UserCompanies>> Create(UserCompanies userCompany)
    {
        try
        {
            var created = await _repo.CreateAsync(userCompany);
            return CreatedAtAction(nameof(Get), new { userId = created.UserId, companyId = created.CompanyId }, created);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("UpdateUserCompany")]
    public async Task<IActionResult> Update([FromQuery] Guid userId, [FromQuery] Guid companyId, UserCompanies userCompany)
    {
        try
        {
            if (userId != userCompany.UserId || companyId != userCompany.CompanyId)
                return BadRequest();
            var updated = await _repo.UpdateAsync(userCompany);
            if (updated == null)
                return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("DeleteUserCompany")]
    public async Task<IActionResult> Delete([FromQuery] Guid userId, [FromQuery] Guid companyId)
    {
        try
        {
            var ok = await _repo.DeleteAsync(userId, companyId);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

