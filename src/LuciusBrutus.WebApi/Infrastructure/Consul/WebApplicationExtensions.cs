namespace LuciusBrutus.WebApi.Infrastructure.Consul;

using global::Consul;
using Microsoft.Extensions.Options;

public static class WebApplicationExtensions
{
    public static void DeregisterConsulAgentOnStop(this WebApplication app)
    {
        app.Lifetime.ApplicationStopped.Register(() =>
        {
            var consulClient = app.Services.GetRequiredService<IConsulClient>();
            var options = app.Services.GetRequiredService<IOptions<ConsulOptions>>().Value;

            if (string.IsNullOrWhiteSpace(options.Environment))
            {
                options.Environment = $"{app.Environment.EnvironmentName}";
            }

            var serviceId = $"{options.Environment}_{options.ServiceName}_{options.ServiceAddress}:{options.ServicePort}";
            consulClient.Agent.ServiceDeregister(serviceId).Wait();
        });
    }

    public static void RegisterConsulAgentOnStart(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var consulClient = app.Services.GetRequiredService<IConsulClient>();
            var options = app.Services.GetRequiredService<IOptions<ConsulOptions>>().Value;

            if (string.IsNullOrWhiteSpace(options.Environment))
            {
                options.Environment = $"{app.Environment.EnvironmentName}";
            }

            var serviceId = $"{options.Environment}_{options.ServiceName}_{options.ServiceAddress}:{options.ServicePort}";

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = options.ServiceName,
                Address = options.ServiceAddress,
                Port = options.ServicePort,
                Tags = options.ServiceTags,
            };

            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();
        });
    }
}
