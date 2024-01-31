﻿using MediatR;
using Images.Application.Common.Exceptions;
using Images.Application.Common.Interfaces;
using Images.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.Application.Features.UserManagement.Queries;
public record GetUserProfileDetailsQuery(string UserId) : IRequest<ApplicationUserDto>;

internal class GetUserProfileDetailsQueryHandler : IRequestHandler<GetUserProfileDetailsQuery, ApplicationUserDto>
{
    private readonly IApplicationUserService _applicationUserService;

    public GetUserProfileDetailsQueryHandler(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

    public async Task<ApplicationUserDto> Handle(GetUserProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var (userId, fullName, userName, email, roles) = await _applicationUserService.GetUserDetailsByUserIdAsync(request.UserId);
        if (userId == null)
        {
            throw new NotFoundException($"User with UserID {request.UserId} does not exist");
        }

        var result = new ApplicationUserDto()
        {
            UserId = userId,
            FullName = fullName,
            Username = userName,
            Email = email,
            Roles = roles
        };

        return result;
    }
}