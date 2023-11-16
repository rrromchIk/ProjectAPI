namespace ProjectAPI.Models;

public class Client {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Project> Projects { get; set; } = null!;
}