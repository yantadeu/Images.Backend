﻿using MediatR;
using Images.Application.Common.Interfaces;
using Images.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.Application.Features.Authentication.Commands.CreateUserCommand;
public record CreateUserCommand : IRequest<string>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<string>? Roles { get; set; } = new List<string> { "Anonymous" };
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(request.FullName!, request.UserName!, request.Email!, request.Password!);

        if (!result.Result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Result.Errors);

            throw new Exception($"Unable to create {request.UserName}.{Environment.NewLine}{errors}");
        }

        var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, request.Roles!);
        if(addUserToRole == null)
        {
            var errors = string.Join(Environment.NewLine, addUserToRole!.Errors);

            throw new Exception($"Unable to add {request.UserName} to assigned role/s.{Environment.NewLine}{errors}");
        }

        return result.UserId;
    }
}
