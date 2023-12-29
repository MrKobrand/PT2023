using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace Web.Blazor.Infrastructure.Impl;

public class HttpRepository : IHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public HttpRepository(HttpClient client)
    {
        _client = client;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public virtual async Task<T> GetRequestAsync<T>(string route, Dictionary<string, string> queryParams = null)
    {
        var queryString = queryParams is null ? route : QueryHelpers.AddQueryString(route, queryParams);
        var response = await _client.GetAsync(queryString);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
    }
}
