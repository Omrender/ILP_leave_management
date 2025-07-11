
namespace LeaveManagementAPI.DTOs;

public class LeaveApprovalDto
{
    public int LeaveRequestId { get; set; }
    public string Status { get; set; } = string.Empty; // "Approved" or "Rejected"
}
