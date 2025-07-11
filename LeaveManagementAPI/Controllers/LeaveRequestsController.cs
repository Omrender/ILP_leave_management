
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using System.Security.Claims;

namespace LeaveManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LeaveRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Employee,Manager")]
    public IActionResult RequestLeave(LeaveRequestDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var request = new LeaveRequest
        {
            UserId = userId,
            LeaveTypeId = dto.LeaveTypeId,
            FromDate = dto.FromDate,
            ToDate = dto.ToDate,
            Reason = dto.Reason,
            Status = "Pending"
        };

        _context.LeaveRequests.Add(request);
        _context.SaveChanges();

        return Ok(request);
    }

    [HttpGet("mine")]
    [Authorize(Roles = "Employee,Manager")]
    public IActionResult GetMyRequests()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requests = _context.LeaveRequests
            .Where(r => r.UserId == userId)
            .ToList();
        return Ok(requests);
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Manager")]
    public IActionResult GetPendingRequests()
    {
        var requests = _context.LeaveRequests
            .Where(r => r.Status == "Pending")
            .ToList();
        return Ok(requests);
    }

    [HttpPut("approve")]
    [Authorize(Roles = "Manager")]
    public IActionResult ApproveRequest(LeaveApprovalDto dto)
    {
        var request = _context.LeaveRequests.Find(dto.LeaveRequestId);
        if (request == null) return NotFound();

        request.Status = dto.Status;
        request.ApprovedById = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        _context.SaveChanges();

        return Ok(request);
    }
}
