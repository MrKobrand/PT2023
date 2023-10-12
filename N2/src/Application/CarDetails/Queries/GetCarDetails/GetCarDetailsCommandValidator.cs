namespace Application.CarDetails.Queries.GetCarDetails;

public class GetCarDetailsCommandValidator : AbstractValidator<GetCarDetailsQuery>
{
    public GetCarDetailsCommandValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThanOrEqualTo(0).WithMessage("Limit at least greater than or equal 0.");

        RuleFor(x => x.Search)
            .MaximumLength(256).WithMessage("Search length must not be more than 256 symbols.");
    }
}
