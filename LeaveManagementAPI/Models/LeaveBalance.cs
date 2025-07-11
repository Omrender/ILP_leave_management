namespace LeaveManagementAPI.Models{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Year { get; set; } = DateTime.UtcNow.Year;
        public int Used { get; set; } = 0;
    }
}
