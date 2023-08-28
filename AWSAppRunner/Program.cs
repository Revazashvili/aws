var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/ping", () => "pong");

app.UseHealthChecks("/health");

app.Run();
