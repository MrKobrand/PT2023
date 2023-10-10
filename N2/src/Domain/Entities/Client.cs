namespace Domain.Entities;

/// <summary>
/// Клиент магазина.
/// </summary>
public class Client : BaseAuditableEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    public string Address { get; set; }
}
