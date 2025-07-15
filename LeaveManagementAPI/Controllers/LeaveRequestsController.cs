
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

    if (dto.Status == "Approved")
    {
        var leaveDays = (request.ToDate - request.FromDate).TotalDays + 1;

        var balance = _context.LeaveBalances.FirstOrDefault(lb =>
            lb.UserId == request.UserId &&
            lb.LeaveTypeId == request.LeaveTypeId &&
            lb.Year == DateTime.UtcNow.Year);

        if (balance == null)
        {
            // DEBUG: show all balances for this user for troubleshooting
            var debugBalances = _context.LeaveBalances
                .Where(lb => lb.UserId == request.UserId)
                .ToList();

            Console.WriteLine($"No matching balance found for UserId={request.UserId}, LeaveTypeId={request.LeaveTypeId}, Year={DateTime.UtcNow.Year}");
            Console.WriteLine($"Existing balances for user: {debugBalances.Count}");
            foreach (var b in debugBalances)
            {
                Console.WriteLine($"→ TypeId={b.LeaveTypeId}, Year={b.Year}, Used={b.Used}");
            }

            return BadRequest("No leave balance found.");
        }

        if ((balance.Used + leaveDays) > 43)
            return BadRequest("Not enough leave balance.");

        Console.WriteLine($"[DEBUG] Leave balance before: {balance.Used}");
        balance.Used += (int)leaveDays;
        Console.WriteLine($"[DEBUG] Leave balance after: {balance.Used}");

        _context.LeaveBalances.Update(balance); // ensure tracked
    }

    request.Status = dto.Status;
    request.ApprovedById = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    _context.LeaveRequests.Update(request); // good to have

    _context.SaveChanges();

    return Ok(request);
}



}
