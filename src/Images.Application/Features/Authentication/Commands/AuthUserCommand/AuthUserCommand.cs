using MediatR;
using Images.Application.Common.Exceptions;
using Images.Application.Common.Interfaces;
using Images.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.Application.Features.Authentication.Commands.AuthUserCommand;

public record AuthUserCommand : IRequest<AuthResponseDto>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IJwtUtils _jwtUtils;

    public AuthUserCommandHandler(IIdentityService identityService, IJwtUtils jwtUtils, IApplicationUserService applicationUserService)
    {
        _identityService = identityService;
        _jwtUtils = jwtUtils;
        _applicationUserService = applicationUserService;
    }

    public async Task<AuthResponseDto> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AuthenticateAsync(request.UserName!, request.Password!);

        if(!result){
            throw new BadRequestException("Invalid username of password");
        }

        var (userId, fullName, userName, email, roles) = await _applicationUserService.GetUserDetailsByUserNameAsync(request.UserName!);

        string token = _jwtUtils.GenerateToken(userId, fullName, userName, roles);

        return new AuthResponseDto(){
            UserId = userId,
            Name = fullName,
            Email = email,
            Roles = roles,
            Token = token
        };
    }
}