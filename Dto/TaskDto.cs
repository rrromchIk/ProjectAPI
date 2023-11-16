using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Dto; 

public class TaskDto {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    [MinLength(2, ErrorMessage = "Description must be at least 2 characters")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Due Date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = null!;
    
    [Required(ErrorMessage = "Assigned Employee Id is required")]
    public int EmployeeId { get; set; }
    
    [Required(ErrorMessage = "Assigned Project Id is required")]
    public int ProjectId { get; set; }
}