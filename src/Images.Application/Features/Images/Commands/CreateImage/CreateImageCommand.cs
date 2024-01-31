using Ardalis.GuardClauses;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Images.Application.Features.Images.Commands.CreateImage;

public record CreateImagesCommand : IRequest<Guid?>
{
    public IFormFile Content { get; set; }
}

public class CreateImagesCommandHandler : IRequestHandler<CreateImagesCommand, Guid?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentHttpRequest _currentHttpRequest;

    public CreateImagesCommandHandler(IApplicationDbContext context, ICurrentHttpRequest currentHttpRequest)
    {
        _context = context;
        _currentHttpRequest = currentHttpRequest;
    }

    public async Task<Guid?> Handle(CreateImagesCommand request, CancellationToken cancellationToken)
    {
        var userId = Guard.Against.NullOrEmpty(_currentHttpRequest.GetUserId());
        var author = Guard.Against.Null(await _context.ApplicationUsers.FindAsync(userId));
        var fileSize = request.Content.Length;
        if (fileSize <= 0) return null;
        using var stream = new MemoryStream();
        await request.Content.CopyToAsync(stream, cancellationToken);
        var bytes = stream.ToArray();
        var hexString = Convert.ToBase64String(bytes);
        var entity = new Image
        {
            Title = Guard.Against.NullOrEmpty(request.Content.FileName),
            Owner = author,
            Content = Guard.Against.NullOrEmpty(hexString),
            ContentType = Guard.Against.NullOrEmpty(request.Content.ContentType),
        };

        _context.Images.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }
}


