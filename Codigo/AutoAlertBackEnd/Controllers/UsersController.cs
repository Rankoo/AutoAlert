using AutoAlertBackEnd.Dtos;
using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoAlertBackEnd.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
    {
        try {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetUser(Guid id)
    {
        try {

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "CREATE_USERS")]
    [HttpPost]
    public async Task<ActionResult<Users>> CreateUser(CreateUserDto newUser)
    {
        try {
            var existingUser = await _userRepository.GetUserByEmailAsync(newUser.Email);
            if (existingUser != null)
                return BadRequest("El usuario ya se encuentra registrado");

            var createdUser = await _userRepository.CreateUserAsync(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "EDIT_USERS")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, Users user)
    {
        try {
            if (id != user.Id)
                return BadRequest();

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            if (updatedUser == null)
                return NotFound();

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "DELETE_USERS")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try {
            var result = await _userRepository.DeleteUserAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet("email/{email}")]
    public async Task<ActionResult<Users>> GetUserByEmail(string email)
    {
        try {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}