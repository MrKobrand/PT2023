using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

/// <summary>
/// Контекст работы с БД.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Набор деталей автомобиля.
    /// </summary>
    DbSet<CarDetail> CarDetails { get; }

    /// <summary>
    /// Набор клиентов.
    /// </summary>
    DbSet<Client> Clients { get; }

    /// <summary>
    /// Сохраняет изменения в контексте.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество измененных сущностей.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
