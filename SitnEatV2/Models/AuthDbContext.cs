using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SitnEatV2.Models
{
	public class AuthDbContext : IdentityDbContext<IdentityUser>
	{
		public AuthDbContext(DbContextOptions options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
