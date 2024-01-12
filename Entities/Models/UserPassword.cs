using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("UsersPasswords", Schema = "backend")]
public class UserPassword : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    
    public User User { get; set; }
}