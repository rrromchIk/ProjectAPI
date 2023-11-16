namespace ProjectAPI.Models;

public class Team {
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = null!;
}