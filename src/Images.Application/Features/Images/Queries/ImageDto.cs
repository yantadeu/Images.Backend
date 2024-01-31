namespace Images.Application.Features.Images.Queries;

public class ImageDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Owner { get; set; }

    public string Content { get; set; } = String.Empty;

    public string ContentType { get; set; } = String.Empty;
    
    public string? DateCreated { get; set; }
}