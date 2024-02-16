using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.Application.Mapping;
using Ordering.Infrastructure.Persistence;
using Ordering.API.Extensions;
using MassTransit;
using EventBus.Messages.Common;
using Ordering.API.EventBusConsumer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Ordering.Application.Model;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------------
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();
builder.Services.Configure<EmailSetting>(x => builder.Configuration.GetSection("EmailSettings"));

//---------------------


//----------------------RabbitMQ---------------------//
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckOutQueue,
            c => { c.ConfigureConsumer<BasketCheckoutConsumer>(ctx); });
    });
});
//--------------------------------------------------//


//------------ identity server ---------------//

//builder.Services.AddAuthentication("bearer").AddJwtBearer("bearer", op =>
//{
//    op.Authority = "https://localhost:7005";// loacl identityServer
//    op.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateAudience = false,
//    };
//});

//------------ identity ---------------//
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        // options.Authority = "https://localhost:7015"; //  Identity.API
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidIssuer = "coin_api",
            ValidAudience = "coin_client",
            ClockSkew = TimeSpan.Zero,
        };
    });
//--------------------------------------------//

// ------------------ swagger opt ------------------//
builder.Services.AddSwaggerGen(op =>
{
    op.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    op.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        },
    });
    op.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering API", Version = "v1" });
});
//-----------------------------------------------//

var app = builder.Build();
//----------------------migrations db -----------------//
//app.MigrateDatabase<Program>();
// app.MigrateDatabase<OrderContext>((context, services) =>
// {
//     var logger = services.GetService<ILogger<OrderContextSeed>>();
//     OrderContextSeed
//         .SeedAsync(context, logger)
//         .Wait();
// });
app.MigrateDatabase<OrderContext>();
// --------------------------------------------------//

//Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering API V1"); });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();