using System;
using System.IO;
using System.Linq;
using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddControllers();
        services.AddResponseCompression(x => x.EnableForHttps = true);
        services.AddMvc();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.OrderActionsBy(description =>
                    $"{description.ActionDescriptor.RouteValues["controller"]}_{description.RelativePath}");

            var xmlDocs = Directory.GetFiles(AppContext.BaseDirectory, "*.xml").ToList();
            xmlDocs.ForEach(xmlDoc => c.IncludeXmlComments(xmlDoc));
        });

        return services;
    }
}
