using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using Domain.Entities;

namespace Application.CarDetails.Commands;

[Authorize(Roles = Roles.ADMINISTRATOR)]
public record CreateCarDetailCommand : IRequest<CarDetail>
{
    public string Name { get; init; }

    public decimal Price { get; init; }
}

public class CreateCarDetailCommandHandler : IRequestHandler<CreateCarDetailCommand, CarDetail>
{
    private readonly IApplicationDbContext _context;

    public CreateCarDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CarDetail> Handle(CreateCarDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = new CarDetail
        {
            Name = request.Name,
            Price = request.Price
        };

        _context.CarDetails.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
