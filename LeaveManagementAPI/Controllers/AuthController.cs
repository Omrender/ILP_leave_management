using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using BCrypt.Net;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(ApplicationDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(user);
        return Ok(new { token, role = user.Role });
    }
}
