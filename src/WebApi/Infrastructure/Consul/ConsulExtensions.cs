namespace LuciusBrutus.WebApi.Infrastructure.Consul;

using global::Consul;

public static class ConsulExtensions
{
    public static void AddConsul(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(ConsulOptions.SECTION_NAME).Get<ConsulOptions>();
        options ??= new ConsulOptions();

        services.Configure<ConsulOptions>(configuration.GetSection(ConsulOptions.SECTION_NAME));

        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri(options.Address);
        }));
    }
}
