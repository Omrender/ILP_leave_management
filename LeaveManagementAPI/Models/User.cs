namespace LeaveManagementAPI.Models{
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = ""; // "Employee" or "Manager"
    public int? ManagerId { get; set; }
    public User? Manager { get; set; }
}
}
