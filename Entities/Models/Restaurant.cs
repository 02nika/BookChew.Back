using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;


[Table("Restaurants", Schema = "backend")]
public class Restaurant : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public User? User { get; set; }
}