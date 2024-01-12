using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.User;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}