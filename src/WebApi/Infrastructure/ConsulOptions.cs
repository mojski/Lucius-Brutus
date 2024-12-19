namespace LuciusBrutus.WebApi.Infrastructure;

public sealed class ConsulOptions
{
    private const string ENVIRONMENT_VARIABLE_KEY = "ASPNETCORE_ENVIRONMENT";

    public const string SECTION_NAME = "Consul";

    public string Address { get; init; } = "http://localhost:8500";

    private string Environment { get; } = GetEnvironment();

    public string ServiceAddress { get; init; } = string.Empty;
    public string ServiceHealthCheckAddress { get; init; } = string.Empty;
    public string ServiceName { get; init; } = string.Empty;
    public int ServicePort { get; init; } = 8500;
    public string[] ServiceTags { get; init; } = default!;
    private string ServiceVersion { get; init; } = string.Empty;

    public string BuildKey()
    {
        var serviceKey = $"{this.Environment}/{this.ServiceName}";

        return serviceKey;
    }

    public string BuildServiceId()
    {
        var serviceId = $"{this.Environment}-{this.ServiceName}-{this.ServiceVersion}-{this.ServiceAddress}-{this.ServicePort}";

        return serviceId;
    }

    private static string GetEnvironment()
    {
        var environment = System.Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_KEY);

        return environment ?? "Development";
    }
}
