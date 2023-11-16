namespace ProjectAPI.Dto; 


public class ProjectDto {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TeamId { get; set; }
    public int ClientId { get; set; }
}