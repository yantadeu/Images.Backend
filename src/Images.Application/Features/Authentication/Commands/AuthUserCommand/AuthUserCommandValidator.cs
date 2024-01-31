using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;

namespace Images.Application.Features.Authentication.Commands.AuthUserCommand;

public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
{
    public AuthUserCommandValidator()
    {
        RuleFor(v => v.UserName)
            .NotEmpty();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}


