using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Extensions;
using WebApp.Model.AccountModel;
using WebApp.Services;
using WebApp.Services.Account;
using WebApp.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddMvc()
// ----------- services ----------------//
builder.Services.AddHttpClient<ICoinService, CoinServeice>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]!);
});
builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]!);
});
//builder.Services.AddHttpClient()
builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddLogging();
builder.Services.AddScoped<IEmailService, EmailService>();
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
    // op.AccessDeniedPath="/Account/AccessDenied"

    op.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});
//-------------------------------------//

var app = builder.Build();

app.MigrateDatabase<DatadbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();