using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;
using Task = ProjectAPI.Models.Task;

namespace ProjectAPI.Data {
    public class DataContext : DbContext {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedEmployee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.AssignedEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedProject)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.AssignedProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}


