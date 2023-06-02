using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SitnEatV2.Models;

namespace SitnEatV2.Models
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
        .ToTable("AspNetUsers")
        .HasKey(u => u.Id);
        }


        //public DbSet<SitnEatV2.Models.Register> Register { get; set; } = default!;

        //public DbSet<SitnEatV2.Models.scaffold> scaffold { get; set; } = default!;

        //public DbSet<UserAccount> userAccounts { get; set; }


    }
}
