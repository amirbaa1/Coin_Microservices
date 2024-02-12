using Identity.API;
using Identity.API.Data;
using Identity.API.Model;
using Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Identity.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add this line to include controller services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ------------- services ---------------//
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("TokenAuthAPI:JWTOption"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ResponseDto>();
// ------------------------------------//

// ---------------------Data ---------------//
builder.Services.AddDbContext<IdentityAppdbContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnectionString"]));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityAppdbContext>()
    .AddDefaultTokenProviders(); // ?? AddIdentity ?? or AddIdentityApiEndpoints for Error 404 GetAPI
//------------------------------------------//

// ----------------- JWT -------------------//
builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("TokenAuthAPI:JWTOption:Secret")!)),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero,
    };
});
//--------------------------------------//
//builder.Services.AddIdentityServer().
//    AddInMemoryClients(Config.Clients).
//    AddInMemoryApiScopes(Config.ApiScopes).
//    AddInMemoryApiResources(Config.ApiResources).
//    AddTestUsers(Config.TestUsers).
//    AddDeveloperSigningCredential();

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnectionString"]);
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = bu => bu.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnectionString"]);


        options.EnableTokenCleanup = true;
        options.TokenCleanupInterval = 3600; // time expe 
    })
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddAspNetIdentity<AppUser>();



builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

var app = builder.Build();
app.MigrateDatabase<IdentityAppdbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();


