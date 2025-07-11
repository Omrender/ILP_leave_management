using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;



namespace LeaveManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, Name = "Sick Leave", DefaultBalance = 10 },
                new LeaveType { Id = 2, Name = "Casual Leave", DefaultBalance = 18 },
                new LeaveType { Id = 3, Name = "Flexible Leave", DefaultBalance = 15 }   
            );
        }

    }
    
}
