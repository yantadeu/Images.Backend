using MediatR;
using Microsoft.AspNetCore.Mvc;
using Images.Application.Common.Models;
using Images.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
using Images.Application.Features.UserManagement.Commands.UpdatePasswordCommand;
using Images.Application.Features.UserManagement.Commands.UpdateEmailCommand;
using Images.Application.Features.UserManagement.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Images.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v1/[controller]")]
[Authorize("Default")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ApplicationUserDto> GetUserDetails(string id)
    {
        var query = new GetUserProfileDetailsQuery(id);

        return await _mediator.Send(query);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateUserDetails(string id, UpdateUserProfileCommand command)
    {
        if (id != command.UserId) return Results.BadRequest("UserId mismatch");

        await _mediator.Send(command);

        return Results.NoContent();
    }

    [HttpPost("{id}/UpdatePassword")]
    public async Task<Result> UpdatePassword(string id, UpdatePasswordCommand command)
    {
        if (id != command.UserId) return Result.Failure(new List<string>{"UserId Mismatch"});

        return await _mediator.Send(command);
    }

    [HttpPost("{id}/UpdateEmail")]
    public async Task<Result> UpdateEmail(string id, UpdateEmailCommand command)
    {
        if (id != command.UserId) return Result.Failure(new List<string>{"UserId Mismatch"});

        return await _mediator.Send(command);
    }

    [HttpGet("{id}/IsAdmin")]
    public async Task<bool> IsAdmin(string id){
        var query = new GetUserIsAdminQuery(id);
        return await _mediator.Send(query);
    }
}

