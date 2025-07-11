namespace LeaveManagementAPI.DTOs;

public class RegisterDto
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "Employee"; // or "Manager"
}
