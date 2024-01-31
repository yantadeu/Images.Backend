using MediatR;
using Microsoft.AspNetCore.Mvc;
using Images.Application.Features.Authentication.Commands.AuthUserCommand;
using Images.Application.Features.Authentication.Commands.CreateUserCommand;

namespace Images.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<string> CreateUser([FromBody] CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("Login")]
    public async Task<AuthResponseDto> Login([FromBody] AuthUserCommand command)
    {
        return await _mediator.Send(command);
    }
}

