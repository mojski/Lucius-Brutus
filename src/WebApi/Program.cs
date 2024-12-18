using LuciusBrutus.WebApi.Infrastructure.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConsul(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.RegisterConsulAgentOnStart();
app.DeregisterConsulAgentOnStop();

app.MapGet("api/test", () => "hello");

app.Run();
