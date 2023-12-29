namespace Application.Clients.Queries.GetClients;

public class GetClientsQueryValidator : AbstractValidator<GetClientsQuery>
{
    public GetClientsQueryValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThanOrEqualTo(0).WithMessage("Limit at least greater than or equal 0.");

        RuleFor(x => x.Search)
            .MaximumLength(256).WithMessage("Search length must not be more than 256 symbols.");
    }
}
