namespace LuciusBrutus.WebApi.Infrastructure.Consul.Extensions;

using global::Consul;

public static class ServiceCollectionExtensions
{
    public static void AddConsulClient(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(ConsulOptions.SECTION_NAME).Get<ConsulOptions>();
        options ??= new ConsulOptions();

        services.AddSingleton(_ => options);

        services.AddSingleton<IConsulClient, ConsulClient>(_ => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri(options.Address);
        }));
    }
}
