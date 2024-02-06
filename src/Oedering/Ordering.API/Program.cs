using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.Application.Mapping;
using Ordering.Infrastructure.Persistence;
using Ordering.API.Extensions;
using MassTransit;
using EventBus.Messages.Common;
using Ordering.API.EventBusConsumer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------------
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();
//---------------------


//----------------------RabbitMQ---------------------//
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckOutQueue,
            c =>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            });
    });
});
//--------------------------------------------------//

var app = builder.Build();

//app.MigrateDatabase<Program>();
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed
        .SeedAsync(context, logger)
        .Wait();
});
//Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
