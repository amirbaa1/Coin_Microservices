using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();


var app = builder.Build();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();