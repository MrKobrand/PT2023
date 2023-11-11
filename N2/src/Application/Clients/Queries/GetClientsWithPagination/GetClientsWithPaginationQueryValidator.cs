namespace Application.Clients.Queries.GetClientsWithPagination;

public class GetClientsWithPaginationQueryValidator : AbstractValidator<GetClientsWithPaginationQuery>
{
    public GetClientsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Search)
            .MaximumLength(256).WithMessage("Search length must not be more than 256 symbols.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
