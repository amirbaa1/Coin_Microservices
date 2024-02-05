using Ordering.Infrastructure;
using Ordering.Application;
using Microsoft.AspNetCore.Hosting;
using MediatR;
using Ordering.Application.Mapping;
using Ordering.Infrastructure.Persistence;
using Ordering.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------------
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
//builder.Services.AddMediatR(typeof(Program));
//builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddAutoMapper(typeof(Program));
//---------------------
var app = builder.Build();

app.MigrationDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed
        .SeedAsync(context, logger)
        .Wait();
});
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
