
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.Data;
using System.Security.Claims;

namespace LeaveManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveBalancesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LeaveBalancesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMyBalances()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var balances = _context.LeaveBalances
            .Where(b => b.UserId == userId && b.Year == DateTime.UtcNow.Year)
            .ToList();
        return Ok(balances);
    }
}
