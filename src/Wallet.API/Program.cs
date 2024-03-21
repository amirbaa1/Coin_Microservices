
using Wallet.API.Data;
using Wallet.API.Services;
using Wallet.API.Services.Coin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient();

// ---------------- serivec --------------//
builder.Services.AddScoped<IWalletdbContext, WalletDbContext>();
builder.Services.AddScoped<IWalletService, WalletService>();
//builder.Services.AddScoped<ICoinService,CoinService>();

builder.Services.AddHttpClient<ICoinService, CoinService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:ApiGateways"]);
});

//--------------------------------------//


//---------Data-----------//

//builder.Services.AddDbContext<WalletDbContext>(op => op.UseSqlServer(builder.Configuration["ConnectionStrings:WalletConnectionString"]));

//-----------------------//



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
