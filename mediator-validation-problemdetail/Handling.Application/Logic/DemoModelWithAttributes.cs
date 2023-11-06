using System.ComponentModel.DataAnnotations;

namespace Handling.Application.Logic;

public class DemoModelWithAttributes
{
    [Required]
    [Range(18, 99)]
    public int Age { get; set; } 
}