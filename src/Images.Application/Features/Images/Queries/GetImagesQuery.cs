using MediatR;
using Microsoft.EntityFrameworkCore;
using Images.Application.Common.Interfaces;

namespace Images.Application.Features.Images.Queries;

public class GetImagesQuery : IRequest<List<ImageDto>>
{
    public ImagesQueryParameters QueryParameters { get; }

    public GetImagesQuery(ImagesQueryParameters queryParameters)
    {
        QueryParameters = queryParameters;
    }
}

public class GetImagesQueryHandler : IRequestHandler<GetImagesQuery, List<ImageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetImagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ImageDto>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Images.AsQueryable();

        // Apply sorting
        if (!string.IsNullOrEmpty(request.QueryParameters.SortBy))
        {
            var propertyName = request.QueryParameters.SortBy;
            var isAscending = string.Equals(request.QueryParameters.SortDirection, "asc", StringComparison.OrdinalIgnoreCase);

            query = isAscending
                ? query.OrderBy(post => post.DateCreated)
                : query.OrderByDescending(post => post.DateCreated);
        }
        else
        {
            query = query.OrderByDescending(post => post.DateCreated);
        }

        // Apply paging
        var skip = (request.QueryParameters.Page - 1) * request.QueryParameters.PageSize;
        var take = request.QueryParameters.PageSize;
        var result = await query
            .Skip(skip)
            .Take(take)
            .Select(b => new ImageDto
            {
                Id = b.Id,
                Title = b.Title,
                Owner = b.Owner.FullName,
                Content = b.Content,
                ContentType = b.ContentType,
                DateCreated = b.DateCreated.ToString("MMMM dd, yyyy")
            })
            .ToListAsync(cancellationToken);

        return result;
    }
}

