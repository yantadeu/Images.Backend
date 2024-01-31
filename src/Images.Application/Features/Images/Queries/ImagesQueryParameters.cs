namespace Images.Application.Features.Images.Queries;
public class ImagesQueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "DateCreated";
    public string? SortDirection { get; set; } = "Desc";
}
