using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
             .Property(o => o.TotalPrice)
             .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        entry.Entity.CreatedBy = "swn";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LasteModeifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "swm";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
