using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;

namespace Images.Application.Features.Authentication.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.FullName)
            .NotEmpty();

        RuleFor(v => v.Email)
            .NotEmpty();

        RuleFor(v => v.UserName)
            .NotEmpty();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}


