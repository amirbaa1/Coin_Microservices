using Identity.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.API.Data
{
    public class IdentityAppdbContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> users { get; set; }
        public IdentityAppdbContext(DbContextOptions<IdentityAppdbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
