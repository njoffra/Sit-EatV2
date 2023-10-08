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
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
        .ToTable("AspNetUsers")
        .HasKey(u => u.Id);
        }

        public DbSet<SitnEatV2.Models.Comment> Comment { get; set; } = default!;


       


    }
}
