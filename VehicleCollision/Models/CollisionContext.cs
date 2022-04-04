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
        //public DbSet<Order> Orders { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlite("Data Source = Bookstore.sqlite");
        //            }
        //        }

        //        protected override void OnModelCreating(ModelBuilder modelBuilder)
        //        {
        //            modelBuilder.Entity<Book>(entity =>
        //            {
        //                entity.HasKey(e => e.BookId);

        //                entity.HasIndex(e => e.BookId)
        //                    .IsUnique();

        //                entity.Property(e => e.BookId)
        //                    .HasColumnName("BookID")
        //                    .ValueGeneratedNever();

        //                entity.Property(e => e.Author).IsRequired();

        //                entity.Property(e => e.Category).IsRequired();

        //                entity.Property(e => e.Classification).IsRequired();

        //                entity.Property(e => e.Isbn)
        //                    .IsRequired()
        //                    .HasColumnName("ISBN");

        //                entity.Property(e => e.Publisher).IsRequired();

        //                entity.Property(e => e.Title).IsRequired();
        //            });

        //            OnModelCreatingPartial(modelBuilder);
        //        }

        //        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
