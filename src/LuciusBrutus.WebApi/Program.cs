using LuciusBrutus.WebApi.Infrastructure.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.RegisterConsulAgentOnStart();
app.DeregisterConsulAgentOnStop();

app.MapGet("api/test", () => "hello");

app.Run();
