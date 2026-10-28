using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entity.Identity;

namespace Store.G04.Repository.Identity.Context
{
    public class StoreIdentityDBContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address> Addresses { get; set; }

        public StoreIdentityDBContext(DbContextOptions<StoreIdentityDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Address");

            builder.Entity<AppUser>()
                .HasOne(u => u.address)
                .WithOne(a => a.AppUser)
                .HasForeignKey<Address>(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}