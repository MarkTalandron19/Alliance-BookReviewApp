﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ASI.Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ASI.Basecode.Data
{
    public partial class AsiBasecodeDBContext : IdentityDbContext<IdentityUser>
    {
        public AsiBasecodeDBContext(DbContextOptions<AsiBasecodeDBContext> options)
            : base(options)
        {
        }

        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshToken.SingleOrDefault(i => i.Username == token.Username);
            if (tokenModel != null)
            {
                RefreshToken.Remove(tokenModel);
                SaveChanges();
            }
            RefreshToken.Add(token);
            SaveChanges();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<BookGenres> Book_Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("AspNetUsers"); 
            });

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

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.genreId);

                entity.Property(e => e.genreName)
                    .IsRequired();

                entity.Property(e => e.description)
                    .IsRequired();

                entity.Property(e => e.CreatedBy);

                entity.Property(e => e.CreatedTime);

                entity.Property(e => e.UpdatedBy);

                entity.Property(e => e.UpdatedTime);

                entity.HasData(
                    new Genre { genreId = "1", genreName = "Science Fiction", description = "Exploring futuristic concepts" },
                    new Genre { genreId = "2", genreName = "Mystery", description = "Intriguing puzzles and suspense" },
                    new Genre { genreId = "3", genreName = "Fantasy", description = "Imaginary worlds and magical elements" },
                    new Genre { genreId = "4", genreName = "Romance", description = "Love and relationships" },
                    new Genre { genreId = "5", genreName = "Thriller", description = "Intense suspense and excitement" }
                );
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.bookId);

                entity.Property(e => e.title)
                    .IsRequired();

                entity.Property(e => e.synopsis)
                    .IsRequired();

                entity.Property(e => e.pubYear)
                    .IsRequired();

                entity.Property(e => e.publisher)
                    .IsRequired();

                entity.Property(e => e.isbn)
                    .IsRequired();

                entity.Property(e => e.language)
                    .IsRequired();

                entity.Property(e => e.image);

                entity.Property(e => e.CreatedBy);

                entity.Property(e => e.CreatedTime);

                entity.Property(e => e.UpdatedBy);

                entity.Property(e => e.UpdatedTime);

                modelBuilder.Entity<Book>().HasData(
                    new Book
                    {
                        bookId = "1",
                        title = "Book 1",
                        synopsis = "Synopsis 1",
                        pubYear = 2000,
                        publisher = "Publisher 1",
                        isbn = "ISBN-1",
                        language = "English"
                    },
                    new Book
                    {
                        bookId = "2",
                        title = "Book 2",
                        synopsis = "Synopsis 2",
                        pubYear = 2000,
                        publisher = "Publisher 2",
                        isbn = "ISBN-2",
                        language = "French"
                    },
                    new Book
                    {
                        bookId = "3",
                        title = "Book 3",
                        synopsis = "Synopsis 3",
                        pubYear = 2000,
                        publisher = "Publisher 3",
                        isbn = "ISBN-3",
                        language = "Spanish"
                    },
                    new Book
                    {
                        bookId = "4",
                        title = "Book 4",
                        synopsis = "Synopsis 4",
                        pubYear = 2000,
                        publisher = "Publisher 4",
                        isbn = "ISBN-4",
                        language = "German"
                    },
                    new Book
                    {
                        bookId = "5",
                        title = "Book 5",
                        synopsis = "Synopsis 5",
                        pubYear = 2000,
                        publisher = "Publisher 5",
                        isbn = "ISBN-5",
                        language = "Italian"
                    }
                );
            });

            modelBuilder.Entity<BookGenres>(entity =>
            {
                entity.HasKey(e => new { e.bookId, e.genreId });

                entity.HasOne(bg => bg.book)
                    .WithMany(b => b.BookGenres)
                    .HasForeignKey(e => e.bookId);

                entity.HasOne(bg => bg.genre)
                    .WithMany(g => g.BookGenres)
                    .HasForeignKey(e => e.genreId);

                entity.HasData(
                    new BookGenres { bookId = "1", genreId = "1" },
                    new BookGenres { bookId = "2", genreId = "2" },
                    new BookGenres { bookId = "3", genreId = "3" },
                    new BookGenres { bookId = "4", genreId = "4" },
                    new BookGenres { bookId = "5", genreId = "5" }
                );
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
