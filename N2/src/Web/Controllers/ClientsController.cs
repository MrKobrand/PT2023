using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Clients.Commands.CreateClient;
using Application.Clients.Queries.GetClients;
using Application.Clients.Queries.GetClientsWithPagination;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class ClientsController : ApiControllerBase
{
    /// <summary>
    /// Получает список клиентов.
    /// </summary>
    /// <param name="query">Параметры запроса.</param>
    /// <returns>Список клиентов.</returns>
    [HttpGet]
    public async Task<IEnumerable<Client>> GetClients([FromQuery] GetClientsQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Получает постраничный вывод клиентов.
    /// </summary>
    /// <param name="query">Параметры запроса.</param>
    /// <returns>Постраничный вывод клиентов.</returns>
    [HttpGet("page")]
    public async Task<PaginatedList<Client>> GetClientsWithPagination([FromQuery] GetClientsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Создает клиента.
    /// </summary>
    /// <param name="command">Параметры запроса.</param>
    /// <returns>Клиент.</returns>
    [HttpPost]
    public async Task<Client> CreateClient([FromBody] CreateClientCommand command)
    {
        return await Mediator.Send(command);
    }
}
