using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Web.Blazor.Infrastructure;
using Web.Blazor.Infrastructure.Impl;
using Web.Blazor.Services;
using Web.Blazor.Services.Impl;

namespace Web.Blazor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<IHttpRepository, HttpRepository>();

        builder.Services.AddScoped<ICarDetailsService, CarDetailsService>();

        await builder.Build().RunAsync();
    }
}
