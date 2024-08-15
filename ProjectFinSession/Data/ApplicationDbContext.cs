using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProjectFinSession.Models;

namespace ProjectFinSession.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<cart> cart { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Nom).IsRequired();
                entity.Property(e => e.Prenom).IsRequired();
                entity.Property(e => e.Adresse).IsRequired();
                entity.HasOne(u => u.Cart)
         .WithOne()
         .HasForeignKey<AppUser>(u => u.CartId)
         .OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<cart>(entity =>
            {
                entity.HasKey(c => c.Id); 
                entity.HasMany(c => c.Products)
                      .WithOne() 
                      .OnDelete(DeleteBehavior.Cascade); 
            });

          
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

        }


    }
}