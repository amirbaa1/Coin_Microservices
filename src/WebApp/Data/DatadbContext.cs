using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Model.AccountModel;

namespace WebApp.Data;

public class DatadbContext : IdentityDbContext<AppUser>
{
    public DatadbContext(DbContextOptions<DatadbContext> options) : base(options)
    {
    }
}