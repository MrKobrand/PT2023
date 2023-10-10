using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Constants;
using Domain.Entities;

namespace Application.Clients.Queries.GetClientsWithPagination;

/// <summary>
/// Контекст, содержащий параметры для запроса, возвращающего список клиентов с пагинацией.
/// </summary>
[Authorize(Roles = Roles.ADMINISTRATOR)]
public class GetClientsWithPaginationQuery : IRequest<PaginatedList<Client>>
{
    /// <summary>
    /// Лимит сущностей.
    /// </summary>
    public int Limit { get; set; } = 25;

    /// <summary>
    /// Строка поиска.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int PageSize { get; set; } = 25;
}

/// <summary>
/// Запрос, возвращающий список клиентов с пагинацией.
/// </summary>
public class GetClientsWithPaginationQueryHandler : IRequestHandler<GetClientsWithPaginationQuery, PaginatedList<Client>>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Создает экземпляр класса <see cref="GetClientsWithPaginationQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст работы с БД.</param>
    public GetClientsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<PaginatedList<Client>> Handle(GetClientsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Client, bool>> searchFilter = x =>
            string.IsNullOrEmpty(request.Search) || x.Name.Contains(request.Search);

        return await _context.Clients
            .Where(searchFilter)
            .Take(request.Limit)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
