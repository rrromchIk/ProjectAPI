namespace ProjectAPI.Models;

public class Employee {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Level { get; set; } = null!;
    public string Role { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = null!;

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
}