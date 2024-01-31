using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Images.Application.Common.Interfaces;

namespace Images.Application.Features.Images.Queries;

public record GetImagesByIdQuery(Guid Id) : IRequest<ImageDto?>;

public class GetImagesByIdQueryHandler : IRequestHandler<GetImagesByIdQuery, ImageDto?>
{
    private readonly IApplicationDbContext _context;

    public GetImagesByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ImageDto?> Handle(GetImagesByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Images
            .Select(b => new ImageDto
            {
                Id = b.Id,
                Title = b.Title,
                Owner = b.Owner.FullName,
                Content = b.Content,
                ContentType = b.ContentType,
                DateCreated = b.DateCreated.ToString("MMMM dd, yyyy")
            })
            .FirstOrDefaultAsync(itm => itm.Id == request.Id, cancellationToken);
            
        return result;            
    }
}