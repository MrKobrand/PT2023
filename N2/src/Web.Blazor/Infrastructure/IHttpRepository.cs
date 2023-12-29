using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Blazor.Infrastructure;

public interface IHttpRepository
{
    Task<T> GetRequestAsync<T>(string route, Dictionary<string, string> queryParams = null);
}
