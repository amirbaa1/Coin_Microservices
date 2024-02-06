using Basket.API.Mapping;
using Basket.API.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------- add Services -------------------//
builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(BasketProfile));
// ----------------------------------------------- //

// --------------- redis --------------------------//
builder.Services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = builder.Configuration.GetValue<string>("ConnectionStrings:localhost");
    op.InstanceName = "Basket.API";
});
// ------------------------------------------------//


// --------------- MassTrasnsit -----------------//

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
//builder.Services.AddMassTransitHostedService(typeof(Program);

//--------------------------------------------//


var app = builder.Build();

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
