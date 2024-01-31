using Ardalis.GuardClauses;
using Images.Application.Common.Interfaces;
using MediatR;

namespace Images.Application.Features.Images.Commands.DeleteImage;

public record DeleteImagesCommand(Guid Id) : IRequest;

public class DeleteImagesCommandHandler : IRequestHandler<DeleteImagesCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteImagesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Images
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Images.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}