namespace Domain.Entities;

/// <summary>
/// Деталь автомобиля.
/// </summary>
public class CarDetail : BaseAuditableEntity
{
    /// <summary>
    /// Стоимость.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }
}
