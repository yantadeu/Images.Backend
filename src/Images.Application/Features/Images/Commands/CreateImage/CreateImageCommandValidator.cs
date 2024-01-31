using FluentValidation;

namespace Images.Application.Features.Images.Commands.CreateImage;

public class CreateImagesCommandValidator : AbstractValidator<CreateImagesCommand>
{
    public CreateImagesCommandValidator()
    {
        RuleFor(v => v.Content)
            .NotEmpty();
    }
}


