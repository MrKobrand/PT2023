namespace Application.CarDetails.Commands;

public class CreateCarDetailCommandValidator : AbstractValidator<CreateCarDetailCommand>
{
    public CreateCarDetailCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Name length must not be more than 256 symbols.");

        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must not be negative.");
    }
}
