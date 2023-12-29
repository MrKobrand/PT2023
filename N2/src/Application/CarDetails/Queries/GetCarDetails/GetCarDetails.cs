using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.CarDetails.Queries.GetCarDetails;

/// <summary>
/// Контекст, содержащий параметры для запроса, возвращающего список автомобильных деталей.
/// </summary>
public record GetCarDetailsQuery : IRequest<IEnumerable<CarDetail>>
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
/// Запрос, возвращающий список автомобильных деталей.
/// </summary>
public class GetCarDetailsQueryHandler : IRequestHandler<GetCarDetailsQuery, IEnumerable<CarDetail>>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Создает экземпляр класса <see cref="GetCarDetailsQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст работы с БД.</param>
    public GetCarDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CarDetail>> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<CarDetail, bool>> searchFilter = x =>
            string.IsNullOrEmpty(request.Search) || x.Name.Contains(request.Search);

        return await _context.CarDetails
            .Where(searchFilter)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
    }
}
