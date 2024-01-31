using System.ComponentModel.DataAnnotations;

namespace Images.Domain.Models;

public class Image : Entity
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = null!;

    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public string ContentType { get; set; } = string.Empty;
}