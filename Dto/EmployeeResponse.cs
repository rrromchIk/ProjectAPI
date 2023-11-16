namespace ProjectAPI.Dto; 

public class EmployeeResponse {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Level { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int TeamId { get; set; }
    
    //public ICollection<TaskDto> Tasks { get; set; } = null!;
}