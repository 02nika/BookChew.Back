using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("Users", Schema = "backend")]
public class User : BaseEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PersonalNumber { get; set; }

    public List<UserPassword> Passwords { get; set; } = [];
    public List<Restaurant> Restaurants { get; set; } = [];
}