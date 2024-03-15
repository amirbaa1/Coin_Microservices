using EventBus.Messages.Common;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System.Reflection;
using Wallet.API.Data;
using Wallet.API.EventBusConsumer;
using Wallet.API.Handlers.CheckWallet;
using Wallet.API.Handlers.UpdateWallet;
using Wallet.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------- serivec --------------//
builder.Services.AddScoped<IWalletdbContext, WalletDbContext>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddAutoMapper(typeof(Program));
;

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IRequestHandler<CheckWalletCommand, ObjectId>>();
builder.Services.AddTransient<IRequestHandler<WalletCommand>>();


//--------------------------------------//


//---------Data-----------//

//builder.Services.AddDbContext<WalletDbContext>(op => op.UseSqlServer(builder.Configuration["ConnectionStrings:WalletConnectionString"]));

//-----------------------//



//----------------------RabbitMQ---------------------//
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<WalletConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.WalletQueue,
            c => { c.ConfigureConsumer<WalletConsumer>(ctx); });
    });
});
//--------------------------------------------------//




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
