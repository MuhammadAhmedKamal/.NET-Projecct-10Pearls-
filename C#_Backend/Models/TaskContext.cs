using Microsoft.EntityFrameworkCore;


namespace Backend_website.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }
        public DbSet<EmployeeTasks> EmployeeTasks { get; set; } 
    }
}