using Basket.API.Mapping;
using Basket.API.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
// --------------- add Services -------------------//
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(BasketProfile));
builder.Services.AddTransient<IBasketService, BasketService>();

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
    config.UsingRabbitMq((ctx, cfg) => { cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]); });
});
//builder.Services.AddMassTransitHostedService(typeof(Program);
//--------------------------------------------//

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
    op.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API V1"); });


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();