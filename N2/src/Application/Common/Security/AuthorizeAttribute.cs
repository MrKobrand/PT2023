using System;

namespace Application.Common.Security;

/// <summary>
/// Указывает на необходимость авторизации на том классе, на котором атрибут применен.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    /// <summary>
    /// Инициализирует объект класса <see cref="AuthorizeAttribute"/>.
    /// </summary>
    public AuthorizeAttribute() { }

    /// <summary>
    /// Получает или устанавливает список ролей, разделенных запятой, которым разрешен доступ к ресурсу.
    /// </summary>
    public string Roles { get; set; } = string.Empty;

    /// <summary>
    /// Получает или устанавливает название политики, которое определяет доступ к ресурсу.
    /// </summary>
    public string Policy { get; set; } = string.Empty;
}
