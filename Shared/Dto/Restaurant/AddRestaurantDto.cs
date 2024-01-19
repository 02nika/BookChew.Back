using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.Restaurant;

public class AddRestaurantDto
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
}