using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownGlobal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HowTo> HowTos { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<TestimonyGroup> TestimonyGroups { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var admin = new IdentityUser
            {
                Id = "a1bcbfc8-241d-4fa9-aac9-de8553158c2b",
                Email = "admin@yopmail.com",
                NormalizedEmail = "admin@yopmail.com",
                UserName = "admin@yopmail.com",
                NormalizedUserName = "admin@yopmail.com",
                EmailConfirmed = true,
                SecurityStamp = "CLRSLGGGHHHXO3OLKAHX3IRNOSYA5F6Z",
                ConcurrencyStamp = "e97a1bd4-7fe5-4824-9217-8df74c239845"
            };

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "administrator");

            modelBuilder.Entity<IdentityUser>().HasData(admin);
        }

    }
}
