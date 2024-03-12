using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Extensions;
using WebApp.Hubs;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Services;
using WebApp.Services.Account;
using WebApp.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddMvc()
// ----------- services ----------------//
builder.Services.AddHttpClient<ICoinService, CoinService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]!);
});
builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]!);
});
builder.Services.AddHttpClient<IBasketService, BasketService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]!);
});


//builder.Services.AddHttpClient()
builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddLogging();
builder.Services.AddScoped<IEmailService, EmailService>();
// builder.Services.AddScoped<IBasketService, BasketService>();
//-------------------------------------//

// ---------------Data --------------------//
builder.Services.AddDbContext<DatadbContext>(op =>
{
    op.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnectionString"]);
});
//-----------------------------------------//

// -------------- cookie! :)) ------------------//
builder.Services.AddIdentity<AppUser, IdentityRole>(op =>
    {
        op.Password.RequiredLength = 8;
        op.Password.RequireLowercase = true;
        op.Password.RequireUppercase = true;

        op.Lockout.MaxFailedAccessAttempts = 5;
        op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

        op.User.RequireUniqueEmail = false;
        op.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<DatadbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/account/login";
    op.LogoutPath = "/account/logout";
    op.AccessDeniedPath = "/Account/AccessDenied";

    op.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy =>
        //policy.RequireRole("V1"));
        policy.RequireClaim("RoleAccount", "Admin"));
});

//-------------------------------------//


builder.Services.AddSignalR();


var app = builder.Build();

app.MigrateDatabase<DatadbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();
app.MapHub<CoinLiveHub>("/hub/coinlive");
app.MapHub<CoinLivePriceHub>("/hub/coinmarket");

app.Run();