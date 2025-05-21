using Microsoft.AspNetCore.Mvc;
using ProductManagerApi.Dtos;
using ProductManagerApi.Helpers;
using ProductManagerApi.Models;
using ProductManagerApi.Repository;

namespace ProductManagerApi.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public UserController(IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDto userDto)
    {
        var user = new UserModel()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Role = userDto.Role,
            IsActive = userDto.IsActive,
            CreatedAt = DateTime.UtcNow,
        };

        return Created("success", _userRepository.Create(user));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserDto userDto)
    {
        var user = _userRepository.GetByEmail(userDto.Email);

        if (user == null) return BadRequest(new { message = "Invalid Credentials" });

        if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
        {
            return BadRequest(new { message = "Invalid Credentials" });
        }

        var jwt = _jwtService.Generate(user.Id);

        Response.Cookies.Append("jwt", jwt, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(new
        {
            message = "success"
        });
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);

            int userId = int.Parse(token.Issuer);

            var user = _userRepository.GetById(userId);

            return Ok(user);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "success"
        });
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();

        if (users == null) return NotFound(new { message = "No users found" });

        return Ok(users);
    }

    [HttpPut("updateuser")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        var user = _userRepository.GetById(userDto.Id);
        if (user == null) return NotFound(new { message = "No user found" });

        user.Name = userDto.Name;
        user.Email = userDto.Email;

        var updatedUser = _userRepository.Update(user);
        return Ok(updatedUser);
    }

    [HttpPut("deactivateuser/{id}")]
    public IActionResult DeactivateUser(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return NotFound(new { message = "No user found" });
        
        user.IsActive = false;
        var updatedUser = _userRepository.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete("deleteuser/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return NotFound(new { message = "No user found" });
        
        _userRepository.Delete(user);
        return Ok(new { message = "User deleted successfully" });
    }
}