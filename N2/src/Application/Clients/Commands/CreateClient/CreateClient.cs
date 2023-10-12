using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using Domain.Entities;

namespace Application.Clients.Commands.CreateClient;

[Authorize(Roles = Roles.ADMINISTRATOR)]
public record CreateClientCommand : IRequest<Client>
{
    public string Name { get; init; }

    public string Address { get; init; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
{
    private readonly IApplicationDbContext _context;

    public CreateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = new Client
        {
            Name = request.Name,
            Address = request.Address
        };

        _context.Clients.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
