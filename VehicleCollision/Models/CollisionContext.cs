using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VehicleCollision.Models
{
    public class CollisionContext : DbContext
    {
        public CollisionContext()
        {
        }

        public CollisionContext(DbContextOptions<CollisionContext> options)
            : base(options)
        {
        }

        public DbSet<Crash> Crashes { get; set; }
        public DbSet<CrashSeverity> CrashSeverity { get; set; }
    }
}
