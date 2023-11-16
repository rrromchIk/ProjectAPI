using ProjectAPI.Models;

namespace ProjectAPI.Dto; 

public class TeamResponse {
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public ICollection<ProjectDto> Projects { get; set; } = null!;
    public ICollection<EmployeeDto> Employees { get; set; } = null!;
}