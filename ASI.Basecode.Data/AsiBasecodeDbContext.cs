using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ASI.Basecode.Data.Models;

namespace ASI.Basecode.Data
{
    public partial class AsiBasecodeDBContext : DbContext
    {
        public AsiBasecodeDBContext()
        {
        }

        public AsiBasecodeDBContext(DbContextOptions<AsiBasecodeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthoredBooks> Authored_Books { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<BookGenres> Book_Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserId, "UQ__Users__1788CC4D5F4A160F")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.reviewId);

                entity.Property(e => e.reviewerFirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.reviewerLastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.reviewerEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.content)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.rating)
                    .IsRequired();

                entity.Property(e => e.dateReviewed)
                    .IsRequired();

                entity.HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(e => e.bookId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.authorId);

                entity.Property(e => e.authorFirstName)
                    .IsRequired();
                
                entity.Property(e => e.authorLastName)
                    .IsRequired();
            });

            modelBuilder.Entity<AuthoredBooks>(entity => 
            {

                entity.HasKey(e => new { e.bookId, e.authorId });

                entity.HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(e => e.bookId);

                entity.HasOne<Author>()
                    .WithMany()
                    .HasForeignKey(e => e.authorId);
            });
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.genreId);

                entity.Property(e => e.genreName)
                    .IsRequired();

                entity.Property(e => e.description)
                    .IsRequired();
            });

            modelBuilder.Entity<BookGenres>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(e => e.bookId);

                entity.HasOne<Genre>()
                .WithMany()
                    .HasForeignKey(e => e.genreId);
            }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
