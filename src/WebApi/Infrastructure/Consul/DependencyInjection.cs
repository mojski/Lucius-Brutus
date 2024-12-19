namespace LuciusBrutus.WebApi.Infrastructure.Consul;

using LuciusBrutus.WebApi.Infrastructure.Consul.Extensions;
using LuciusBrutus.WebApi.Infrastructure.Consul.Services;

public static class DependencyInjection
{
    public static void AddConsulIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsulClient(configuration);
        services.AddHostedService<ConsulHostedService>();
    }

    public static void UseConsul(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddConsulKeyValueConfiguration();
    }
}
