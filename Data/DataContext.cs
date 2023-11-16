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
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); // Delete task when deleting project

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction); // No cascading delete when deleting employee

            modelBuilder.Entity<Employee>()
                .Property(e => e.TeamId)
                .IsRequired(false);
            
            modelBuilder.Entity<Project>()
                .Property(e => e.TeamId)
                .IsRequired(false);
            
            modelBuilder.Entity<Project>()
                .Property(e => e.ClientId)
                .IsRequired(false);
            
            modelBuilder.Entity<Task>()
                .Property(e => e.EmployeeId)
                .IsRequired(false);
        }
    }
}


