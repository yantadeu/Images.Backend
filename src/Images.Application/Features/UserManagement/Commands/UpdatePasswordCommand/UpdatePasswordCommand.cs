using MediatR;
using Images.Application.Common.Interfaces;
using Images.Application.Common.Models;

namespace Images.Application.Features.UserManagement.Commands.UpdatePasswordCommand;
public class UpdatePasswordCommand : IRequest<Result>
{
    public string? UserId { get;set; }

    public string? OldPassword { get; set;}

    public string? NewPassword { get; set; }
}

internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
{
    private readonly IIdentityService _identityService;

    public UpdatePasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.UpdatePasswordAsync(request.UserId!, request.OldPassword!, request.NewPassword!);
    }
}
