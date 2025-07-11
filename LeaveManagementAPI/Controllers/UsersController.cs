using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.Data;
using LeaveManagementAPI.Models;
using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        if (_context.Users.Any(u => u.Email == dto.Email))
            return BadRequest("User already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        // Assign default leave balances (total = 43)
        var leaveTypes = _context.LeaveTypes.ToList();
        foreach (var type in leaveTypes)
        {
            _context.LeaveBalances.Add(new LeaveBalance
            {
                UserId = user.Id,
                LeaveTypeId = type.Id,
                Year = DateTime.UtcNow.Year,
                Used = 0
            });
        }

        _context.SaveChanges();
        return Ok("User created with default leave balances.");
    }
}
