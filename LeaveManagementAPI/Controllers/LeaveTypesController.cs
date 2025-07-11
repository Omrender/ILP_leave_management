
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.Data;

namespace LeaveManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LeaveTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetLeaveTypes()
    {
        var types = _context.LeaveTypes.ToList();
        return Ok(types);
    }
}
