using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Web.Blazor.Services;

public interface ICarDetailsService
{
    Task<IEnumerable<CarDetail>> GetListAsync(int? limit = null, string search = null);
}
