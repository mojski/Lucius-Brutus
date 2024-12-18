namespace LuciusBrutus.WebApi.Infrastructure;

public sealed class ConsulOptions
{
    public static readonly string SECTION_NAME = "Consul";
    public string Address { get; init; } = "http://localhost:8500";
    public string Environment { get; set; } = string.Empty;
    public string ServiceAddress { get; init; } = string.Empty;
    public string ServiceHealthCheckAddress { get; init; } = string.Empty;
    public string ServiceName { get; init; } = string.Empty;
    public int ServicePort { get; init; } = 8500;
    public string[] ServiceTags { get; init; } = default!;

    public string GetServiceId()
    {
        var serviceId = $"{this.Environment}-{this.ServiceName}-{this.ServiceAddress}-{this.ServicePort}";

        return serviceId;
    }
}
