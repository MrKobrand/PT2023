using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetClients;

/// <summary>
/// Контекст, содержащий параметры для запроса, возвращающего список клиентов.
/// </summary>
[Authorize(Roles = Roles.ADMINISTRATOR)]
public record GetClientsQuery : IRequest<IEnumerable<Client>>
{
    /// <summary>
    /// Лимит сущностей.
    /// </summary>
    public int Limit { get; set; } = 25;

    /// <summary>
    /// Строка поиска.
    /// </summary>
    public string Search { get; set; }
}

/// <summary>
/// Запрос, возвращающий список клиентов.
/// </summary>
public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Создает экземпляр класса <see cref="GetClientsQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст работы с БД.</param>
    public GetClientsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Client>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Client, bool>> searchFilter = x =>
            string.IsNullOrEmpty(request.Search) || x.Name.Contains(request.Search);

        return await _context.Clients
            .Where(searchFilter)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
    }
}
