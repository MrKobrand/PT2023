using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CarDetails.Commands.CreateCarDetail;
using Application.CarDetails.Queries.GetCarDetails;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class CarDetailsController : ApiControllerBase
{
    /// <summary>
    /// Получает список автомобильных деталей.
    /// </summary>
    /// <param name="query">Параметры запроса.</param>
    /// <returns>Список автомобильных деталей.</returns>
    [HttpGet]
    public async Task<IEnumerable<CarDetail>> GetCarDetails([FromQuery] GetCarDetailsQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Создает автомобильную деталь.
    /// </summary>
    /// <param name="command">Параметры запроса.</param>
    /// <returns>Автомобильная деталь.</returns>
    [HttpPost]
    [Authorize]
    public async Task<CarDetail> CreateCarDetail([FromBody] CreateCarDetailCommand command)
    {
        return await Mediator.Send(command);
    }
}
