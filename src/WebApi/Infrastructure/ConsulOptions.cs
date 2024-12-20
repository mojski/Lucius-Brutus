namespace LuciusBrutus.WebApi.Infrastructure;

public sealed class ConsulOptions
{
    public const string SECTION_NAME = "Consul";
    private const string SERVICE_NAME = "LuciusBrutus";
    private const string SERVICE_VERSION = "1.0";

    public string Address { get; init; } = "http://localhost:8500";
    public string InstanceName { get; init; } = "local";
    public int PollWaitTimeInSeconds { get; init; } = 5;
    public string ServiceAddress { get; init; } = string.Empty;
    public string ServiceHealthCheckAddress { get; init; } = string.Empty;
    public string ServiceName => SERVICE_NAME;
    public int ServicePort { get; init; } = 8500;
    public string[] ServiceTags { get; init; } = default!;

    public string BuildKey()
    {
        var serviceKey = $"{this.InstanceName}/{SERVICE_NAME}";

        return serviceKey;
    }

    public string BuildServiceId()
    {
        var serviceId = $"{this.InstanceName}-{SERVICE_NAME}-{SERVICE_VERSION}-{this.ServiceAddress}-{this.ServicePort}";

        return serviceId;
    }
}
