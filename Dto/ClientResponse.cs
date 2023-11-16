using ProjectAPI.Models;

namespace ProjectAPI.Dto; 

public class ClientResponse {
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<ProjectDto> Projects { get; set; } = null!;
}