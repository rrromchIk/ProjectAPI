using System.Collections;

namespace ProjectAPI.Dto; 

public class ProjectResponse {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TeamId { get; set; }
    public int ClientId { get; set; }

    public ICollection<TaskDto> Tasks { get; set; } = null!;
}