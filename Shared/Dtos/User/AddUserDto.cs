using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.User;

public class AddUserDto
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }
    
    [Required]
    [MinLength(2)]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(2)]
    public string UserName { get; set; }
    
    [Required]
    [MinLength(5)]
    public string PersonalNumber { get; set; }
    
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}