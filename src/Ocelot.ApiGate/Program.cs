using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

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


//builder.Services.AddOcelot();


IWebHostEnvironment webHostEnvironment = builder.Environment;
builder.Configuration.SetBasePath(webHostEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddOcelot(webHostEnvironment)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration)
    .AddPolly()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });


var app = builder.Build();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();