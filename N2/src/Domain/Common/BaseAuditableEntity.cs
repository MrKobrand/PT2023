using System;

namespace Domain.Common;

/// <summary>
/// Базовая сущность.
/// </summary>
public abstract class BaseAuditableEntity : IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    public DateTimeOffset CreateDate { get; set; }

    /// <summary>
    /// Кем создано.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Дата обновления сущности.
    /// </summary>
    public DateTimeOffset UpdateDate { get; set; }

    /// <summary>
    /// Кем обновлено.
    /// </summary>
    public string UpdatedBy { get; set; }
}
