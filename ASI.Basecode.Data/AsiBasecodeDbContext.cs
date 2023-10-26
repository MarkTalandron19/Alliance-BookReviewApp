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
        public virtual DbSet<Book> Books { get; set; }
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

                entity.Property(e => e.CreatedBy);

                entity.Property(e => e.CreatedTime);

                entity.Property(e => e.UpdatedBy);

                entity.Property(e => e.UpdatedTime);

                modelBuilder.Entity<Author>().HasData(
                    new Author { authorId = "1", authorFirstName = "John", authorLastName = "Doe" },
                    new Author { authorId = "2", authorFirstName = "Jane", authorLastName = "Smith" },
                    new Author { authorId = "3", authorFirstName = "Robert", authorLastName = "Johnson" },
                    new Author { authorId = "4", authorFirstName = "Emily", authorLastName = "Williams" },
                    new Author { authorId = "5", authorFirstName = "David", authorLastName = "Brown" }
                );
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
                        pubYear = DateTime.Now,
                        publisher = "Publisher 1",
                        isbn = "ISBN-1",
                        language = "English"
                    },
                    new Book
                    {
                        bookId = "2",
                        title = "Book 2",
                        synopsis = "Synopsis 2",
                        pubYear = DateTime.Now,
                        publisher = "Publisher 2",
                        isbn = "ISBN-2",
                        language = "French"
                    },
                    new Book
                    {
                        bookId = "3",
                        title = "Book 3",
                        synopsis = "Synopsis 3",
                        pubYear = DateTime.Now,
                        publisher = "Publisher 3",
                        isbn = "ISBN-3",
                        language = "Spanish"
                    },
                    new Book
                    {
                        bookId = "4",
                        title = "Book 4",
                        synopsis = "Synopsis 4",
                        pubYear = DateTime.Now,
                        publisher = "Publisher 4",
                        isbn = "ISBN-4",
                        language = "German"
                    },
                    new Book
                    {
                        bookId = "5",
                        title = "Book 5",
                        synopsis = "Synopsis 5",
                        pubYear = DateTime.Now,
                        publisher = "Publisher 5",
                        isbn = "ISBN-5",
                        language = "Italian"
                    }
                );
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

                entity.HasData(
                    new AuthoredBooks { bookId = "1", authorId = "1" },
                    new AuthoredBooks { bookId = "2", authorId = "2" },
                    new AuthoredBooks { bookId = "3", authorId = "3" },
                    new AuthoredBooks { bookId = "4", authorId = "4" },
                    new AuthoredBooks { bookId = "5", authorId = "5" }
                );
            });

            modelBuilder.Entity<BookGenres>(entity =>
            {
                entity.HasKey(e => new { e.bookId, e.genreId });

                entity.HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(e => e.bookId);

                entity.HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey(e => e.genreId);

                entity.HasData(
                    new BookGenres { bookId = "1", genreId = "1" },
                    new BookGenres { bookId = "2", genreId = "2" },
                    new BookGenres { bookId = "3", genreId = "3" },
                    new BookGenres { bookId = "4", genreId = "4" },
                    new BookGenres { bookId = "5", genreId = "5" }
                );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
