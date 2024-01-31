using FluentValidation;

namespace Images.Application.Features.Images.Commands.DeleteImage;

public class DeleteImagesCommandValidator : AbstractValidator<DeleteImagesCommand>
{
    public DeleteImagesCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}


