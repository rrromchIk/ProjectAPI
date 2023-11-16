namespace ProjectAPI.Dto; 

public class TeamResponse {
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public ICollection<ProjectDto> Projects { get; set; }
}