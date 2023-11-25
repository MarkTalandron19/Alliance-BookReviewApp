using System;
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

                entity.HasOne(e => e.book)
                    .WithMany(b => b.Reviews)
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
                title = "Miss Peregrine's Home for Peculiar Children",
                synopsis = "When Jacob discovers clues to a mystery that spans different worlds and times, he finds a magical place known as Miss Peregrine's Home for Peculiar Children.",
                pubYear = "2011",
                publisher = "Quirk Books",
                isbn = "ISBN-9781594746031",
                language = "English",
                author = "Ransom Riggs",
                image = "/seeder/1.jpg"
            },
            new Book
            {
                bookId = "2",
                title = "Hollow City: The Second Novel of Miss Peregrine's Peculiar Children",
                synopsis = "Having escaped Miss Peregrine's island by the skin of their teeth, Jacob and his new friends journey to London, the peculiar capital of the world.",
                pubYear = "2014",
                publisher = "Quirk Books",
                isbn = "ISBN-9781594747359",
                language = "English",
                author = "Ransom Riggs",
                image = "/seeder/2.jpg"
            },
            new Book
            {
                bookId = "3",
                title = "Library of Souls: The Third Novel of Miss Peregrine's Peculiar Children",
                synopsis = "A boy with extraordinary powers. An army of deadly monsters. An epic battle for the future of peculiardom.",
                pubYear = "2015",
                publisher = "Quirk Books",
                isbn = "ISBN-9781594747588",
                language = "English",
                author = "Ransom Riggs",
                image = "/seeder/3.jpg"
            },
            new Book
            {
                bookId = "4",
                title = "A Map of Days: The Fourth Novel of Miss Peregrine's Peculiar Children",
                synopsis = "Having defeated the monstrous threat that nearly destroyed the peculiar world, Jacob Portman is back where his story began, in Florida.",
                pubYear = "2018",
                publisher = "Penguin Random House",
                isbn = "ISBN-9780735232143",
                language = "English",
                author = "Ransom Riggs",
                image = "/seeder/4.jpg"
            },
            new Book
            {
                bookId = "5",
                title = "The Conference of the Birds: The Fifth Novel of Miss Peregrine's Peculiar Children",
                synopsis = "The adventure that began with Miss Peregrine's Home for Peculiar Children and continued in A Map of Days comes to a thrilling conclusion.",
                pubYear = "2020",
                publisher = "Penguin Random House",
                isbn = "ISBN-9780735232144",
                language = "English",
                author = "Ransom Riggs",
                image = "/seeder/5.jpg"
            }, new Book
            {
                bookId = "6",
                title = "The Silent Patient",
                synopsis = "Alicia Berenson's life is seemingly perfect until she shoots her husband and then never speaks another word. Theo Faber, a criminal psychotherapist, is determined to unravel her mystery.",
                pubYear = "2019",
                publisher = "Celadon Books",
                isbn = "ISBN-9781250301697",
                language = "English",
                author = "Alex Michaelides",
                image = "/seeder/6.jpg"
            },
            new Book
            {
                bookId = "7",
                title = "Where the Crawdads Sing",
                synopsis = "A mystery tale of Kya Clark, known as the Marsh Girl, accused of the murder of Chase Andrews in the quiet town of Barkley Cove.",
                pubYear = "2018",
                publisher = "G.P. Putnam's Sons",
                isbn = "ISBN-9780735219091",
                language = "English",
                author = "Delia Owens",
                image = "/seeder/7.jpg"
            },
            new Book
            {
                bookId = "8",
                title = "The Night Circus",
                synopsis = "Two young illusionists, Celia and Marco, are bound in a magical competition, unaware that it's a battle where only one can survive.",
                pubYear = "2011",
                publisher = "Doubleday",
                isbn = "ISBN-9780307744432",
                language = "English",
                author = "Erin Morgenstern",
                image = "/seeder/8.jpg"
            },
            new Book
            {
                bookId = "9",
                title = "Educated",
                synopsis = "A memoir recounting Tara Westover's inspiring journey from growing up in a strict survivalist family in Idaho to earning a Ph.D. from Cambridge University.",
                pubYear = "2018",
                publisher = "Random House",
                isbn = "ISBN-9780399590504",
                language = "English",
                author = "Tara Westover",
                image = "/seeder/9.jpg"
            },
            new Book
            {
                bookId = "10",
                title = "The Great Gatsby",
                synopsis = "Jay Gatsby's extravagant parties and his pursuit of Daisy Buchanan, set against the backdrop of 1920s Long Island, ultimately leading to tragic consequences.",
                pubYear = "1925",
                publisher = "Charles Scribner's Sons",
                isbn = "ISBN-9780743273565",
                language = "English",
                author = "F. Scott Fitzgerald",
                image = "/seeder/10.jpg"
            },
            new Book
            {
                bookId = "11",
                title = "The Alchemist",
                synopsis = "Follows the journey of Santiago, an Andalusian shepherd boy, as he travels in search of a worldly treasure.",
                pubYear = "1988",
                publisher = "HarperCollins",
                isbn = "ISBN-9780062315007",
                language = "English",
                author = "Paulo Coelho",
                image = "/seeder/11.jpg"
            },
            new Book
            {
                bookId = "12",
                title = "To Kill a Mockingbird",
                synopsis = "A classic novel set in the American South during the 1930s, dealing with the issues of racial injustice and moral growth through the eyes of Scout Finch.",
                pubYear = "1960",
                publisher = "J. B. Lippincott & Co.",
                isbn = "ISBN-9780061120084",
                language = "English",
                author = "Harper Lee",
                image = "/seeder/12.jpg"
            },
            new Book
            {
                bookId = "13",
                title = "1984",
                synopsis = "George Orwell's dystopian novel explores a totalitarian society controlled by a single party led by Big Brother.",
                pubYear = "1949",
                publisher = "Secker & Warburg",
                isbn = "ISBN-9780451524935",
                language = "English",
                author = "George Orwell",
                image = "/seeder/13.jpg"
            },
            new Book
            {
                bookId = "14",
                title = "The Catcher in the Rye",
                synopsis = "Narrates the experiences of Holden Caulfield, a disenchanted teenager navigating life and dealing with societal phoniness.",
                pubYear = "1951",
                publisher = "Little, Brown and Company",
                isbn = "ISBN-9780316769488",
                language = "English",
                author = "J.D. Salinger",
                image = "/seeder/14.jpg"
            },
            new Book
            {
                bookId = "15",
                title = "The Hobbit",
                synopsis = "The story of Bilbo Baggins, a hobbit who embarks on an adventurous journey to help a group of dwarves reclaim their homeland.",
                pubYear = "1937",
                publisher = "Allen & Unwin",
                isbn = "ISBN-9780261102217",
                language = "English",
                author = "J.R.R. Tolkien",
                image = "/seeder/15.jpg"
            },
            new Book
            {
                bookId = "16",
                title = "The First Days",
                synopsis = "Katie is driving to work one beautiful day when a dead man jumps into her car and tries to eat her.  That same morning, Jenni opens a bedroom door to find her husband devouring their toddler son. ",
                pubYear = "2008",
                publisher = "Tor Books",
                isbn = "ISBN-9780765331267",
                language = "English",
                author = "Rhiannon Frater",
                image = "/seeder/16.jpg"
            },
            new Book
            {
                bookId = "17",
                title = "Pride and Prejudice",
                synopsis = "A romantic novel revolving around Elizabeth Bennet and Mr. Darcy, exploring societal norms, love, and misunderstandings.",
                pubYear = "1813",
                publisher = "T. Egerton, Whitehall",
                isbn = "ISBN-9780141199078",
                language = "English",
                author = "Jane Austen",
                image = "/seeder/17.jpg"
            },
            new Book
            {
                bookId = "18",
                title = "Brave New World",
                synopsis = "A dystopian novel portraying a future society controlled by technology, conformity, and conditioning.",
                pubYear = "1932",
                publisher = "Chatto & Windus",
                isbn = "ISBN-9780099518471",
                language = "English",
                author = "Aldous Huxley",
                image = "/seeder/18.jpg"
            },
            new Book
            {
                bookId = "19",
                title = "The Lord of the Rings",
                synopsis = "A fantasy epic that chronicles the quest to destroy the One Ring and defeat the Dark Lord Sauron.",
                pubYear = "1954",
                publisher = "Allen & Unwin",
                isbn = "ISBN-9780261102361",
                language = "English",
                author = "J.R.R. Tolkien",
                image = "/seeder/19.jpg"
            },
            new Book
            {
                bookId = "20",
                title = "The Chronicles of Narnia",
                synopsis = "A series of high-fantasy novels depicting the adventures in the magical land of Narnia.",
                pubYear = "1950",
                publisher = "Geoffrey Bles",
                isbn = "ISBN-9780066238500",
                language = "English",
                author = "C.S. Lewis",
                image = "/seeder/20.jpg"
            },
            new Book
            {
                bookId = "21",
                title = "Rich Dad Poor Dad",
                synopsis = "The book explores the mindset and financial knowledge needed to succeed financially, based on the author's experiences.",
                pubYear = "1997",
                publisher = "Plata Publishing",
                isbn = "ISBN-9781612680194",
                language = "English",
                author = "Robert T. Kiyosaki",
                image = "/seeder/21.jpg"
            },
            new Book
            {
                bookId = "22",
                title = "Atomic Habits",
                synopsis = "A guide to creating good habits, breaking bad ones, and mastering the tiny behaviors that lead to remarkable results.",
                pubYear = "2018",
                publisher = "Avery",
                isbn = "ISBN-9780735211293",
                language = "English",
                author = "James Clear",
                image = "/seeder/22.jpg"
            },
            new Book
            {
                bookId = "23",
                title = "Gates of Fire",
                synopsis = "A historical novel that retells the story of the Battle of Thermopylae from the perspective of the Spartans.",
                pubYear = "1998",
                publisher = "Bantam",
                isbn = "ISBN-9780553580532",
                language = "English",
                author = "Steven Pressfield",
                image = "/seeder/23.jpg"
            },
            new Book
            {
                bookId = "24",
                title = "Moby-Dick",
                synopsis = "An epic saga of Captain Ahab's obsessive quest for revenge against the white whale, Moby Dick.",
                pubYear = "1851",
                publisher = "Richard Bentley",
                isbn = "ISBN-9780142437247",
                language = "English",
                author = "Herman Melville",
                image = "/seeder/24.jpg"
            },
            new Book
            {
                bookId = "25",
                title = "The Hitchhiker's Guide to the Galaxy",
                synopsis = "A comedic science fiction series following the misadventures of an unwitting human, Arthur Dent.",
                pubYear = "1979",
                publisher = "Pan Books",
                isbn = "ISBN-9780345391803",
                language = "English",
                author = "Douglas Adams",
                image = "/seeder/25.jpg"
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
