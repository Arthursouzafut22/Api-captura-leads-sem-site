using LeadSemSite.Application.Interfaces;
using LeadSemSite.Infrastructure.ExternalServices.Geocoding;
using LeadSemSite.Infrastructure.ExternalServices.Serper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeadSemSite.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddMemoryCache();

        services.AddHttpClient<IGeocodingClient, NominatimClient>(c =>
        {
            c.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
            c.DefaultRequestHeaders.UserAgent.ParseAdd("leadsemsite/1.0 (contato@seuemail.com)");
        });

        services.AddHttpClient<ISerperClient, SerperClient>(c =>
        {
            c.BaseAddress = new Uri("https://google.serper.dev/");
            c.DefaultRequestHeaders.Add("X-API-KEY", config["Serper:ApiKey"]);
        });

        return services;
    }
}