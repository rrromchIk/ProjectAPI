using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Dto; 

public class ClientDto {
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Contact Person is required")]
    [MaxLength(50, ErrorMessage = "Contact Person cannot exceed 50 characters")]
    [MinLength(2, ErrorMessage = "Contact Person must be at least 2 characters")]
    public string ContactPerson { get; set; } = null!;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Phone Number is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; } = null!;
}