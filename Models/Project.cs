namespace ProjectAPI.Models; 

public class Project {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
    
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = null!;
}