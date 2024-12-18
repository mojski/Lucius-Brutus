using LuciusBrutus.WebApi.Infrastructure.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConsulClient(builder.Configuration);
builder.Configuration.AddConsulKeyValueConfiguration();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.RegisterConsulAgentOnStart();
app.DeregisterConsulAgentOnStop();

app.MapGet("api/test", () => "hello");

app.Run();
