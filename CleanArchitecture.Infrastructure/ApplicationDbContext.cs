

using ComplexCalculator.Domain.Entities;
using ComplexCalculator.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add a DbSet for
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Calculator> Calculators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Optional: Add any specific configurations for the UserSession table here
            builder.Entity<UserSession>().ToTable("UserSessions");
            builder.Entity<Calculator>(entity =>
            {
                entity.Property(e => e.UserId)
                      .HasMaxLength(450)      // Set nvarchar(450)
                      .IsUnicode(true);        // By default, nvarchar is Unicode
            });


            // Optional: Configure relationships if needed (e.g., one-to-many between ApplicationUser and UserSession)
        }
    }
}
