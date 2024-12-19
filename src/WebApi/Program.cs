using LuciusBrutus.WebApi.Infrastructure.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConsulIntegration(builder.Configuration);
builder.Configuration.UseConsul();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("api/test", () => "hello");

app.Run();
