

using LeaveManagementAPI.Models;
namespace LeaveManagementAPI.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; } = "";
        public string Status { get; set; } = "Pending"; // "Approved", "Rejected"
        public int? ApprovedById { get; set; }
        public User? ApprovedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}