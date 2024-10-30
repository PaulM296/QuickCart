using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickCart.Domain.Entities;
using QuickCart.Infrastructure.Configurations;

namespace QuickCart.Infrastructure.Data
{
    public class QuickCartDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {    
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
