## Lucius Brutus 

Consul integration with .NET app.

1. Install required packages:
 - Winton.Extensions.Configuration.Consul

2. Run docker compose from "infra" directory (it will run consul in container)
3. Run the app from ide or cmd.
4. You can use docker compose from src directory it start web api and consul within same network 

## main features
 ### Service discovery 
 To add web api to consul it need few steps to be performed:
 - register consul client in in service collection ServiceCollectionExtensions (AddConsulClient method) with proper configuration
 - register on start and deregister on stop with WebApplicationExtensions class methods
 - configure healthcheck 

 ```csharp
    //...
 builder.Services.AddHealthChecks();
    //...
app.MapHealthChecks("/health");
    //...
```
- add consul health check agent during registration in RegisterConsulAgentOnStart method:

```csharp
    //...
var registration = new AgentServiceRegistration
{
    ID = serviceId,
    Name = options.ServiceName,
    Address = options.ServiceAddress,
    Port = options.ServicePort,
    Check = new AgentServiceCheck
    {
        HTTP = options.ServiceHealthCheckAddress,
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(1),
    },
};
    //...
```

- do not forget configure ServiceHealthCheckAddress health check endpoint in app settings - if health check is configured and it is available for consul it will check health of application

### Configuration by key value feature

- save some configuration in consul UI 

![alt text](/readme_assets/image.png)

- use AddConsulKeyValueConfiguration method from ConfigurationBuilderExtensions class 

From now you can read configuration form consul server:

```csharp
var configuration = app.Services.GetService<IConfiguration>();

var config = configuration.GetValue<string>("key");  // config will be "value"
```
Note: serviceKey is built in ConsulOptions class must be same as set in consul:

![alt text](/readme_assets/image-1.png)

it has directory structure: with Develompent directory contains LuciusBrutus dir, so key returnet by ConsulOptions must be "Development/LuciusBrutus"

## Read more

[Consul in .NET — A Service Mesh Solution And Service Discovery Tool](https://medium.com/@KeivanDamirchi/consul-in-net-a-service-mesh-solution-and-service-discovery-tool-eff18292c771)


