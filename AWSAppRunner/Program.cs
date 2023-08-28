var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHealthChecks("/health");

app.Run();