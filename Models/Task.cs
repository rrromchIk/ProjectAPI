namespace ProjectAPI.Models;

public class Task {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = null!;

    public int AssignedEmployeeId { get; set; }
    public Employee AssignedEmployee { get; set; } = null!;

    public int AssignedProjectId { get; set; }
    public Project AssignedProject { get; set; } = null!;
}