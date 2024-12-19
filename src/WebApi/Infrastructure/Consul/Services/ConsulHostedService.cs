namespace LuciusBrutus.WebApi.Infrastructure.Consul.Services;

using global::Consul;

internal sealed class ConsulHostedService : IHostedService
{
    private readonly IConsulClient consulClient;
    private readonly ConsulOptions options;

    public ConsulHostedService(IConsulClient consulClient, ConsulOptions options)
    {
        this.consulClient = consulClient;
        this.options = options;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceId = this.options.BuildServiceId();

        var registration = new AgentServiceRegistration
        {
            ID = serviceId,
            Name = this.options.ServiceName,
            Address = this.options.ServiceAddress,
            Port = this.options.ServicePort,
            Tags = this.options.ServiceTags,
            Check = new AgentServiceCheck
            {
                HTTP = this.options.ServiceHealthCheckAddress,
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(1),
            },
        };

        try
        {
            await this.consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
        }
        catch
        {
            // ignore
        }

        await this.consulClient.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var serviceId = this.options.BuildServiceId();
        await this.consulClient.Agent.ServiceDeregister(serviceId, cancellationToken);
    }
}
