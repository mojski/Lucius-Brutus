namespace LuciusBrutus.WebApi.Infrastructure.Consul;

using Winton.Extensions.Configuration.Consul;

public static class ConfigurationBuilderExtensions
{
    public static void AddConsulKeyValueConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        var configuration = configurationBuilder.Build();
        var consulOptions = configuration.GetSection(ConsulOptions.SECTION_NAME).Get<ConsulOptions>() ?? new ConsulOptions();

        var key = consulOptions.BuildKey();

        configurationBuilder.AddConsul(key,
            options =>
            {
                options.ConsulConfigurationOptions = cco => cco.Address = new Uri(consulOptions.Address);
                options.Optional = true;
                options.PollWaitTime = TimeSpan.FromSeconds(5);
                options.ReloadOnChange = true;
            });
    }
}
