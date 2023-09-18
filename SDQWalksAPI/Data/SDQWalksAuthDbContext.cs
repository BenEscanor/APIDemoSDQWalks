using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SDQWalksAPI.Data
{
    public class SDQWalksAuthDbContext : IdentityDbContext
    {
        public SDQWalksAuthDbContext(DbContextOptions<SDQWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "9ca075a0-71e1-4fb3-849b-49db0d63c261";
            var writerId = "29afae57-dde9-4ad1-b93a-6842fd0b0d2e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                { Id = writerId,
                  ConcurrencyStamp= writerId,
                  Name = "Writer",
                  NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
