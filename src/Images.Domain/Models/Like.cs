using System.ComponentModel.DataAnnotations;

namespace Images.Domain.Models;

public class Like : Entity
{
    [Required]
    public ApplicationUser Owner { get; set; } = null!;

    [Required]
    public Image Image { get; set; } = null!;

    public bool Liked { get; set; } = false;
}