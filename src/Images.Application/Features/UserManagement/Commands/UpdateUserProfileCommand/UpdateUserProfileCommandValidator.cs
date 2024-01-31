using FluentValidation;
using MediatR;
using Images.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;
public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.FullName)
            .MaximumLength(255)
            .NotEmpty();
    }
   
}
