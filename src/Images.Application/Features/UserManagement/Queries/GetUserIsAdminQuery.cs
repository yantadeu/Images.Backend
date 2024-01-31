using MediatR;
using Images.Application.Common.Interfaces;
using Images.Domain.Constants;

namespace Images.Application.Features.UserManagement.Queries;

public record GetUserIsAdminQuery(string UserId) : IRequest<bool>;

public class GetUserIsAdminQueryHandler : IRequestHandler<GetUserIsAdminQuery, bool>
{
    private readonly IIdentityService _identityService;

    public GetUserIsAdminQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(GetUserIsAdminQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.IsInRoleAsync(request.UserId, Roles.Administrator);
    }
}
