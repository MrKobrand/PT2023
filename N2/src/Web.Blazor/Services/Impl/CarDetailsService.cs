using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Web.Blazor.Infrastructure;

namespace Web.Blazor.Services.Impl;

public class CarDetailsService : ICarDetailsService
{
    private readonly IHttpRepository _httpRepository;

    private const string ROUTE = "api/car-details";

    public CarDetailsService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    public Task<IEnumerable<CarDetail>> GetListAsync(int? limit = null, string search = null)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["Limit"] = limit.HasValue ? limit.Value.ToString() : "25",
            ["Search"] = search ?? string.Empty
        };

        return _httpRepository.GetRequestAsync<IEnumerable<CarDetail>>(ROUTE, queryParams);
    }
}
