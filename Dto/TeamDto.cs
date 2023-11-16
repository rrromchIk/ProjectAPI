using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Dto;

public class TeamDto {
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; } = null!;
}