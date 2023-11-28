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

        public virtual DbSet<User> CustomUsers {  get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<BookGenres> Book_Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasData(
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "Superadmin",
                        NormalizedName = "SUPERADMIN",
                    },
                    new IdentityRole
                    {
                        Id = "2",
                        Name = "Genremaster",
                        NormalizedName = "GENREMASTER",
                    },
                    new IdentityRole
                    {
                        Id = "3",
                        Name = "Bookmaster",
                        NormalizedName = "BOOKMASTER",
                    });  
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

                entity.HasData(
                    new User
                    {
                        Id = 1,
                        Email = "admin@gmail.com",
                        UserId = "admin",
                        Name = "admin",
                        Password = "Admin1!",
                        CreatedBy = System.Environment.UserName,
                        UpdatedBy = System.Environment.UserName,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
            });
            });

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                var passwordHasher = new PasswordHasher<IdentityUser>();
                entity.ToTable("AspNetUsers"); 
                entity.HasData(
                    new IdentityUser
                    {
                        Id = "e5b4ff19-cc2c-4e46-86e9-8b702eb16526",
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = passwordHasher.HashPassword(null, "Admin1!")
                    });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasData(
                    new IdentityUserRole<string>
                    {
                        UserId = "e5b4ff19-cc2c-4e46-86e9-8b702eb16526",
                        RoleId = "1",
                    });
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
                    .IsUnicode(false);

                entity.Property(e => e.rating)
                    .IsRequired();

                entity.Property(e => e.dateReviewed)
                    .IsRequired();

                entity.HasOne(e => e.book)
                    .WithMany(b => b.Reviews)
                    .HasForeignKey(e => e.bookId);

                entity.HasData(
    new Review { 
        reviewId = "1", 
        reviewerEmail = "test@gmail.com", 
        reviewerFirstName = "test", 
        reviewerLastName = "test", 
        dateReviewed = DateTime.Now,
        content = "Sample content", 
        rating = 4,
        bookId = "1", 
    },
    new Review
    {
        reviewId = "2",
        reviewerEmail = "test2@gmail.com",
        reviewerFirstName = "Aloysius",
        reviewerLastName = "Beronque",
        dateReviewed = DateTime.Now,
        content = "Wow this is a great book!",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "3",
        reviewerEmail = "test3@gmail.com",
        reviewerFirstName = "Karen",
        reviewerLastName = "Miller",
        dateReviewed = DateTime.Now,
        content = "Slow pacing, got bored of it immediately.",
        rating = 2,
        bookId = "1",
    },
    new Review
    {
        reviewId = "4",
        reviewerEmail = "www@gmail.com",
        reviewerFirstName = "Aloysius",
        reviewerLastName = "Beronque",
        dateReviewed = DateTime.Now,
        content = "Very nice Story. Impressive writing by the Author.",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "5",
        reviewerEmail = "qwerty@gmail.com",
        reviewerFirstName = "Enrique",
        reviewerLastName = "Pacudan",
        dateReviewed = DateTime.Now,
        content = "A good read.",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "6",
        reviewerEmail = "abc@gmail.com",
        reviewerFirstName = "Aaron",
        reviewerLastName = "Alcuitas",
        dateReviewed = DateTime.Now,
        content = "Not the best out of everything, but still very good nonetheless. I slightly teared up right before the ending part.",
        rating = 4,
        bookId = "1",
    },
    new Review
    {
        reviewId = "7",
        reviewerEmail = "xyz@gmail.com",
        reviewerFirstName = "Emily",
        reviewerLastName = "Johnson",
        dateReviewed = DateTime.Now,
        content = "Captivating storyline! It kept me engaged till the end.",
        rating = 3,
        bookId = "1",
    },
    new Review
    {
        reviewId = "8",
        reviewerEmail = "user123@gmail.com",
        reviewerFirstName = "David",
        reviewerLastName = "Smith",
        dateReviewed = DateTime.Now,
        content = "An emotional rollercoaster! Loved the character development.",
        rating = 4,
        bookId = "1",
    },
    new Review
    {
        reviewId = "9",
        reviewerEmail = "booklover@gmail.com",
        reviewerFirstName = "Sophie",
        reviewerLastName = "Brown",
        dateReviewed = DateTime.Now,
        content = "Didn't meet my expectations, but still a decent read.",
        rating = 3,
        bookId = "1",
    },
    new Review
    {
        reviewId = "10",
        reviewerEmail = "avidreader@gmail.com",
        reviewerFirstName = "Jack",
        reviewerLastName = "Miller",
        dateReviewed = DateTime.Now,
        content = "Enchanting world-building and a compelling storyline.",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "11",
        reviewerEmail = "reader123@gmail.com",
        reviewerFirstName = "Olivia",
        reviewerLastName = "Davis",
        dateReviewed = DateTime.Now,
        content = "The ending left me wanting more! Fantastic book.",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "12",
        reviewerEmail = "fictionlover@gmail.com",
        reviewerFirstName = "Lucas",
        reviewerLastName = "Thompson",
        dateReviewed = DateTime.Now,
        content = "A bit slow-paced but beautifully written.",
        rating = 4,
        bookId = "1",
    },
    new Review
    {
        reviewId = "13",
        reviewerEmail = "mysteryreader@gmail.com",
        reviewerFirstName = "Ella",
        reviewerLastName = "Wilson",
        dateReviewed = DateTime.Now,
        content = "Thrilling plot twists! Kept me guessing till the end.",
        rating = 5,
        bookId = "1",
    },
    new Review
    {
        reviewId = "14",
        reviewerEmail = "adventureseeker@gmail.com",
        reviewerFirstName = "Nathan",
        reviewerLastName = "Adams",
        dateReviewed = DateTime.Now,
        content = "An adventurous journey with captivating characters.",
        rating = 4,
        bookId = "1",
    },
    new Review
    {
        reviewId = "15",
        reviewerEmail = "literaturefan@gmail.com",
        reviewerFirstName = "Lily",
        reviewerLastName = "Robinson",
        dateReviewed = DateTime.Now,
        content = "A good balance of mystery and fantasy elements.",
        rating = 4,
        bookId = "1",
    },
    new Review
    {
        reviewId = "16",
        reviewerEmail = "imaginative@gmail.com",
        reviewerFirstName = "William",
        reviewerLastName = "Garcia",
        dateReviewed = DateTime.Now,
        content = "Immersive world, though the pacing felt uneven at times.",
        rating = 3,
        bookId = "1",
    },
    new Review
    {
        reviewId = "17",
        reviewerEmail = "fantasylover@gmail.com",
        reviewerFirstName = "Sophia",
        reviewerLastName = "Thompson",
        dateReviewed = DateTime.Now,
        content = "A fantastic continuation of the series! Loved the new adventures.",
        rating = 5,
        bookId = "2",
    },
    new Review
    {
        reviewId = "18",
        reviewerEmail = "readerreviewer@gmail.com",
        reviewerFirstName = "Daniel",
        reviewerLastName = "Roberts",
        dateReviewed = DateTime.Now,
        content = "Engaging storyline with intriguing characters.",
        rating = 4,
        bookId = "2",
    },
    new Review
    {
        reviewId = "19",
        reviewerEmail = "mysteryfan@gmail.com",
        reviewerFirstName = "Eva",
        reviewerLastName = "Lee",
        dateReviewed = DateTime.Now,
        content = "The plot twists kept me on the edge of my seat!",
        rating = 5,
        bookId = "2",
    },
    new Review
    {
        reviewId = "20",
        reviewerEmail = "avidreader2@gmail.com",
        reviewerFirstName = "Michael",
        reviewerLastName = "Anderson",
        dateReviewed = DateTime.Now,
        content = "Immersive and well-crafted world-building.",
        rating = 4,
        bookId = "2",
    },
    new Review
    {
        reviewId = "21",
        reviewerEmail = "fantasyadventurer@gmail.com",
        reviewerFirstName = "Ava",
        reviewerLastName = "Martinez",
        dateReviewed = DateTime.Now,
        content = "Magical and adventurous! Loved every page.",
        rating = 5,
        bookId = "2",
    },
    new Review
    {
        reviewId = "22",
        reviewerEmail = "storybooklover@gmail.com",
        reviewerFirstName = "Noah",
        reviewerLastName = "Gonzalez",
        dateReviewed = DateTime.Now,
        content = "A bit slow-paced but a solid addition to the series.",
        rating = 3,
        bookId = "2",
    },
    new Review
    {
        reviewId = "23",
        reviewerEmail = "adventurebookfan@gmail.com",
        reviewerFirstName = "Grace",
        reviewerLastName = "Hernandez",
        dateReviewed = DateTime.Now,
        content = "Interesting twists with charming peculiarities.",
        rating = 4,
        bookId = "2",
    },
    new Review
    {
        reviewId = "24",
        reviewerEmail = "fantasyaddict@gmail.com",
        reviewerFirstName = "Ethan",
        reviewerLastName = "Perez",
        dateReviewed = DateTime.Now,
        content = "Fascinating world-building and character development.",
        rating = 5,
        bookId = "2",
    },
    new Review
    {
        reviewId = "25",
        reviewerEmail = "bookworm@gmail.com",
        reviewerFirstName = "Emma",
        reviewerLastName = "Flores",
        dateReviewed = DateTime.Now,
        content = "A thrilling journey with unexpected surprises.",
        rating = 4,
        bookId = "2",
    },
    new Review
    {
        reviewId = "26",
        reviewerEmail = "fantasylover2@gmail.com",
        reviewerFirstName = "Liam",
        reviewerLastName = "Wood",
        dateReviewed = DateTime.Now,
        content = "Good storytelling despite a slow start.",
        rating = 3,
        bookId = "2",
    },
    new Review
    {
        reviewId = "27",
        reviewerEmail = "peculiarfanatic@gmail.com",
        reviewerFirstName = "Olivia",
        reviewerLastName = "Evans",
        dateReviewed = DateTime.Now,
        content = "An epic finale to an extraordinary series!",
        rating = 5,
        bookId = "3",
    },
    new Review
    {
        reviewId = "28",
        reviewerEmail = "paranormalreader@gmail.com",
        reviewerFirstName = "Jack",
        reviewerLastName = "Collins",
        dateReviewed = DateTime.Now,
        content = "The adventure and suspense kept me hooked till the end.",
        rating = 4,
        bookId = "3",
    },
    new Review
    {
        reviewId = "29",
        reviewerEmail = "fantasylover3@gmail.com",
        reviewerFirstName = "Sophie",
        reviewerLastName = "Baker",
        dateReviewed = DateTime.Now,
        content = "A satisfying conclusion to a fantastic journey.",
        rating = 5,
        bookId = "3",
    },
    new Review
    {
        reviewId = "30",
        reviewerEmail = "adventureseeker@gmail.com",
        reviewerFirstName = "Lucas",
        reviewerLastName = "Diaz",
        dateReviewed = DateTime.Now,
        content = "Imaginative storytelling with a fitting end.",
        rating = 4,
        bookId = "3",
    },
    new Review
    {
        reviewId = "31",
        reviewerEmail = "mysteryfan2@gmail.com",
        reviewerFirstName = "Ella",
        reviewerLastName = "Hill",
        dateReviewed = DateTime.Now,
        content = "Intriguing plot twists and intense moments.",
        rating = 4,
        bookId = "3",
    },
    new Review
    {
        reviewId = "32",
        reviewerEmail = "fantasyadventurer2@gmail.com",
        reviewerFirstName = "Logan",
        reviewerLastName = "Ward",
        dateReviewed = DateTime.Now,
        content = "A thrilling and satisfying conclusion.",
        rating = 5,
        bookId = "3",
    },
    new Review
    {
        reviewId = "33",
        reviewerEmail = "bookworm2@gmail.com",
        reviewerFirstName = "Mia",
        reviewerLastName = "Rivera",
        dateReviewed = DateTime.Now,
        content = "A rollercoaster of emotions and adventures.",
        rating = 5,
        bookId = "3",
    },
    new Review
    {
        reviewId = "34",
        reviewerEmail = "fantasyaddict2@gmail.com",
        reviewerFirstName = "Aiden",
        reviewerLastName = "Cruz",
        dateReviewed = DateTime.Now,
        content = "An epic journey with memorable characters.",
        rating = 5,
        bookId = "3",
    },
    new Review
    {
        reviewId = "35",
        reviewerEmail = "storylover@gmail.com",
        reviewerFirstName = "Lily",
        reviewerLastName = "Patterson",
        dateReviewed = DateTime.Now,
        content = "An enjoyable ride filled with suspense.",
        rating = 4,
        bookId = "3",
    },
    new Review
    {
        reviewId = "36",
        reviewerEmail = "fictionenthusiast@gmail.com",
        reviewerFirstName = "Owen",
        reviewerLastName = "Ramirez",
        dateReviewed = DateTime.Now,
        content = "Engaging and thrilling till the very end!",
        rating = 4,
        bookId = "3",
    },
    new Review
    {
        reviewId = "37",
        reviewerEmail = "adventurelover@gmail.com",
        reviewerFirstName = "Ethan",
        reviewerLastName = "Williams",
        dateReviewed = DateTime.Now,
        content = "A captivating continuation of the peculiar series.",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "38",
        reviewerEmail = "fantasyfanatic@gmail.com",
        reviewerFirstName = "Madison",
        reviewerLastName = "Harris",
        dateReviewed = DateTime.Now,
        content = "The peculiar world continues to enchant and surprise!",
        rating = 5,
        bookId = "4",
    },
    new Review
    {
        reviewId = "39",
        reviewerEmail = "peculiarenthusiast@gmail.com",
        reviewerFirstName = "David",
        reviewerLastName = "Martin",
        dateReviewed = DateTime.Now,
        content = "Riveting storytelling with new intriguing elements.",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "40",
        reviewerEmail = "bookworm3@gmail.com",
        reviewerFirstName = "Ava",
        reviewerLastName = "Garcia",
        dateReviewed = DateTime.Now,
        content = "An engaging and peculiar journey with familiar characters.",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "41",
        reviewerEmail = "fantasyreader3@gmail.com",
        reviewerFirstName = "Elijah",
        reviewerLastName = "Adams",
        dateReviewed = DateTime.Now,
        content = "Fascinating and full of unexpected turns!",
        rating = 5,
        bookId = "4",
    },
    new Review
    {
        reviewId = "42",
        reviewerEmail = "adventureseeker2@gmail.com",
        reviewerFirstName = "Grace",
        reviewerLastName = "Stewart",
        dateReviewed = DateTime.Now,
        content = "A peculiarly delightful addition to the series.",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "43",
        reviewerEmail = "peculiarfan2@gmail.com",
        reviewerFirstName = "Xavier",
        reviewerLastName = "Evans",
        dateReviewed = DateTime.Now,
        content = "A journey that continues to surprise and mesmerize!",
        rating = 5,
        bookId = "4",
    },
    new Review
    {
        reviewId = "44",
        reviewerEmail = "fantasylover4@gmail.com",
        reviewerFirstName = "Luna",
        reviewerLastName = "Collins",
        dateReviewed = DateTime.Now,
        content = "An imaginative and captivating continuation.",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "45",
        reviewerEmail = "mysteryadventurer@gmail.com",
        reviewerFirstName = "Nolan",
        reviewerLastName = "Diaz",
        dateReviewed = DateTime.Now,
        content = "Continues to immerse readers into the peculiar world!",
        rating = 4,
        bookId = "4",
    },
    new Review
    {
        reviewId = "46",
        reviewerEmail = "fantasyaddict3@gmail.com",
        reviewerFirstName = "Scarlett",
        reviewerLastName = "Rivera",
        dateReviewed = DateTime.Now,
        content = "Enthralling storytelling that keeps you hooked!",
        rating = 5,
        bookId = "4",
    },
    new Review
    {
        reviewId = "47",
        reviewerEmail = "adventureenthusiast@gmail.com",
        reviewerFirstName = "Noah",
        reviewerLastName = "Williams",
        dateReviewed = DateTime.Now,
        content = "A thrilling and fitting conclusion to the peculiar series.",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "48",
        reviewerEmail = "fantasyfanatic2@gmail.com",
        reviewerFirstName = "Chloe",
        reviewerLastName = "Harris",
        dateReviewed = DateTime.Now,
        content = "An epic culmination to an extraordinary journey!",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "49",
        reviewerEmail = "peculiarenthusiast2@gmail.com",
        reviewerFirstName = "Caleb",
        reviewerLastName = "Martin",
        dateReviewed = DateTime.Now,
        content = "A satisfying and adventurous conclusion.",
        rating = 4,
        bookId = "5",
    },
    new Review
    {
        reviewId = "50",
        reviewerEmail = "bookworm4@gmail.com",
        reviewerFirstName = "Emma",
        reviewerLastName = "Garcia",
        dateReviewed = DateTime.Now,
        content = "A mesmerizing finale with unexpected twists.",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "51",
        reviewerEmail = "fantasyreader4@gmail.com",
        reviewerFirstName = "Ethan",
        reviewerLastName = "Adams",
        dateReviewed = DateTime.Now,
        content = "A captivating end to an enchanting series!",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "52",
        reviewerEmail = "adventureseeker3@gmail.com",
        reviewerFirstName = "Zoe",
        reviewerLastName = "Stewart",
        dateReviewed = DateTime.Now,
        content = "A fantastic and fulfilling conclusion.",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "53",
        reviewerEmail = "peculiarfan3@gmail.com",
        reviewerFirstName = "Mason",
        reviewerLastName = "Evans",
        dateReviewed = DateTime.Now,
        content = "An epic and spellbinding ending!",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "54",
        reviewerEmail = "fantasylover5@gmail.com",
        reviewerFirstName = "Liam",
        reviewerLastName = "Collins",
        dateReviewed = DateTime.Now,
        content = "An extraordinary finale with magical elements.",
        rating = 5,
        bookId = "5",
    },
    new Review
    {
        reviewId = "55",
        reviewerEmail = "mysteryadventurer2@gmail.com",
        reviewerFirstName = "Aria",
        reviewerLastName = "Diaz",
        dateReviewed = DateTime.Now,
        content = "A fitting end to a captivating journey!",
        rating = 4,
        bookId = "5",
    },
    new Review
    {
        reviewId = "56",
        reviewerEmail = "fantasyaddict4@gmail.com",
        reviewerFirstName = "Isabella",
        reviewerLastName = "Rivera",
        dateReviewed = DateTime.Now,
        content = "A delightful conclusion filled with adventure!",
        rating = 4,
        bookId = "5",
    },
    new Review
    {
        reviewId = "57",
        reviewerEmail = "mysterylover@gmail.com",
        reviewerFirstName = "Lucas",
        reviewerLastName = "Brown",
        dateReviewed = DateTime.Now,
        content = "An intense psychological thriller with an unexpected twist.",
        rating = 5,
        bookId = "6",
    },
    new Review
    {
        reviewId = "58",
        reviewerEmail = "suspensefanatic@gmail.com",
        reviewerFirstName = "Sophie",
        reviewerLastName = "Miller",
        dateReviewed = DateTime.Now,
        content = "A gripping narrative that keeps you guessing until the end.",
        rating = 5,
        bookId = "6",
    },
    new Review
    {
        reviewId = "59",
        reviewerEmail = "psychologyenthusiast@gmail.com",
        reviewerFirstName = "Adam",
        reviewerLastName = "Wilson",
        dateReviewed = DateTime.Now,
        content = "A mind-bending thriller that leaves you in suspense.",
        rating = 4,
        bookId = "6",
    },
    new Review
    {
        reviewId = "60",
        reviewerEmail = "mysteryreader@gmail.com",
        reviewerFirstName = "Ava",
        reviewerLastName = "Garcia",
        dateReviewed = DateTime.Now,
        content = "An intriguing story with well-crafted characters.",
        rating = 4,
        bookId = "6",
    },
    new Review
    {
        reviewId = "61",
        reviewerEmail = "suspenseaddict@gmail.com",
        reviewerFirstName = "Liam",
        reviewerLastName = "Thompson",
        dateReviewed = DateTime.Now,
        content = "A suspenseful and thrilling ride from start to finish.",
        rating = 5,
        bookId = "6",
    },
    new Review
    {
        reviewId = "62",
        reviewerEmail = "naturelover@gmail.com",
        reviewerFirstName = "Emma",
        reviewerLastName = "Green",
        dateReviewed = DateTime.Now,
        content = "Beautifully written, capturing the essence of nature and emotion.",
        rating = 5,
        bookId = "7",
    },
    new Review
    {
        reviewId = "63",
        reviewerEmail = "storyappreciator@gmail.com",
        reviewerFirstName = "Noah",
        reviewerLastName = "Baker",
        dateReviewed = DateTime.Now,
        content = "A compelling narrative blending mystery and nature seamlessly.",
        rating = 4,
        bookId = "7",
    },
    new Review
    {
        reviewId = "64",
        reviewerEmail = "mysteryenthusiast@gmail.com",
        reviewerFirstName = "Chloe",
        reviewerLastName = "Hill",
        dateReviewed = DateTime.Now,
        content = "An engrossing story with a vividly depicted setting.",
        rating = 4,
        bookId = "7",
    },
    new Review
    {
        reviewId = "65",
        reviewerEmail = "booklover@gmail.com",
        reviewerFirstName = "Mason",
        reviewerLastName = "Reed",
        dateReviewed = DateTime.Now,
        content = "Immersive storytelling that transports you to another world.",
        rating = 5,
        bookId = "7",
    },
    new Review
    {
        reviewId = "66",
        reviewerEmail = "emotionalexperience@gmail.com",
        reviewerFirstName = "Sophia",
        reviewerLastName = "Foster",
        dateReviewed = DateTime.Now,
        content = "A touching and atmospheric tale of nature and humanity.",
        rating = 5,
        bookId = "7",
    },
    new Review
    {
        reviewId = "67",
        reviewerEmail = "magicenthusiast@gmail.com",
        reviewerFirstName = "Oliver",
        reviewerLastName = "Wright",
        dateReviewed = DateTime.Now,
        content = "An enchanting story with a magical ambiance.",
        rating = 5,
        bookId = "8",
    },
    new Review
    {
        reviewId = "68",
        reviewerEmail = "fantasylover@gmail.com",
        reviewerFirstName = "Isabella",
        reviewerLastName = "Hughes",
        dateReviewed = DateTime.Now,
        content = "Captivating and beautifully written. The magic feels real!",
        rating = 5,
        bookId = "8",
    },
    new Review
    {
        reviewId = "69",
        reviewerEmail = "whimsicalreader@gmail.com",
        reviewerFirstName = "Gabriel",
        reviewerLastName = "Bell",
        dateReviewed = DateTime.Now,
        content = "A mesmerizing and delightful read, perfect for fantasy lovers.",
        rating = 5,
        bookId = "8",
    },
    new Review
    {
        reviewId = "70",
        reviewerEmail = "storydreamer@gmail.com",
        reviewerFirstName = "Hannah",
        reviewerLastName = "Jenkins",
        dateReviewed = DateTime.Now,
        content = "Immersive and magical; a unique and fantastical experience.",
        rating = 4,
        bookId = "8",
    },
    new Review
    {
        reviewId = "71",
        reviewerEmail = "enchantedtales@gmail.com",
        reviewerFirstName = "Lucas",
        reviewerLastName = "Parker",
        dateReviewed = DateTime.Now,
        content = "A beautifully woven narrative with an air of mystique.",
        rating = 5,
        bookId = "8",
    },
    new Review
    {
        reviewId = "72",
        reviewerEmail = "inspiringjourney@gmail.com",
        reviewerFirstName = "Ava",
        reviewerLastName = "Woods",
        dateReviewed = DateTime.Now,
        content = "An inspiring and thought-provoking memoir about resilience.",
        rating = 5,
        bookId = "9",
    },
    new Review
    {
        reviewId = "73",
        reviewerEmail = "lifechangingbooks@gmail.com",
        reviewerFirstName = "Jack",
        reviewerLastName = "Cooper",
        dateReviewed = DateTime.Now,
        content = "A powerful story of determination and the pursuit of education.",
        rating = 5,
        bookId = "9",
    },
    new Review
    {
        reviewId = "74",
        reviewerEmail = "memoirlover@gmail.com",
        reviewerFirstName = "Lily",
        reviewerLastName = "Gordon",
        dateReviewed = DateTime.Now,
        content = "A moving and insightful memoir about overcoming obstacles.",
        rating = 4,
        bookId = "9",
    },
    new Review
    {
        reviewId = "75",
        reviewerEmail = "motivatingreads@gmail.com",
        reviewerFirstName = "Nathan",
        reviewerLastName = "Reyes",
        dateReviewed = DateTime.Now,
        content = "Compelling and inspiring; a testament to the human spirit.",
        rating = 5,
        bookId = "9",
    },
    new Review
    {
        reviewId = "76",
        reviewerEmail = "strengthinwords@gmail.com",
        reviewerFirstName = "Emma",
        reviewerLastName = "Barnes",
        dateReviewed = DateTime.Now,
        content = "An incredible journey that showcases the power of education.",
        rating = 5,
        bookId = "9",
    },
    new Review
    {
        reviewId = "77",
        reviewerEmail = "roaring20s@gmail.com",
        reviewerFirstName = "Leo",
        reviewerLastName = "Fitzgerald",
        dateReviewed = DateTime.Now,
        content = "A poignant portrayal of the Jazz Age and its disillusionment.",
        rating = 5,
        bookId = "10",
    },
    new Review
    {
        reviewId = "78",
        reviewerEmail = "classicslover@gmail.com",
        reviewerFirstName = "Ella",
        reviewerLastName = "Thompson",
        dateReviewed = DateTime.Now,
        content = "A classic novel capturing the essence of a bygone era.",
        rating = 5,
        bookId = "10",
    },
    new Review
    {
        reviewId = "79",
        reviewerEmail = "gatsbyfanatic@gmail.com",
        reviewerFirstName = "Oliver",
        reviewerLastName = "White",
        dateReviewed = DateTime.Now,
        content = "A captivating story of love, wealth, and the American Dream.",
        rating = 4,
        bookId = "10",
    },
    new Review
    {
        reviewId = "80",
        reviewerEmail = "literaryenthusiast@gmail.com",
        reviewerFirstName = "Sophia",
        reviewerLastName = "Baker",
        dateReviewed = DateTime.Now,
        content = "A profound tale of tragedy and the pursuit of happiness.",
        rating = 4,
        bookId = "10",
    },
    new Review
    {
        reviewId = "81",
        reviewerEmail = "readerfromthepast@gmail.com",
        reviewerFirstName = "Jack",
        reviewerLastName = "Williams",
        dateReviewed = DateTime.Now,
        content = "An emblematic novel embodying the extravagance of the 1920s.",
        rating = 5,
        bookId = "10",
    },
    new Review
    {
        reviewId = "82",
        reviewerEmail = "dreamchasers@gmail.com",
        reviewerFirstName = "Santiago",
        reviewerLastName = "Perez",
        dateReviewed = DateTime.Now,
        content = "A transformative journey that inspires and enlightens.",
        rating = 5,
        bookId = "11",
    },
    new Review
    {
        reviewId = "83",
        reviewerEmail = "adventureseeker@gmail.com",
        reviewerFirstName = "Sophie",
        reviewerLastName = "Adams",
        dateReviewed = DateTime.Now,
        content = "A captivating tale of discovery and self-realization.",
        rating = 4,
        bookId = "11",
    },
    new Review
    {
        reviewId = "84",
        reviewerEmail = "inspirationquest@gmail.com",
        reviewerFirstName = "Elijah",
        reviewerLastName = "Clark",
        dateReviewed = DateTime.Now,
        content = "An allegorical masterpiece filled with wisdom and wonder.",
        rating = 5,
        bookId = "11",
    },
    new Review
    {
        reviewId = "85",
        reviewerEmail = "seekerofmeaning@gmail.com",
        reviewerFirstName = "Isabella",
        reviewerLastName = "Lopez",
        dateReviewed = DateTime.Now,
        content = "A thought-provoking narrative about destiny and purpose.",
        rating = 4,
        bookId = "11",
    },
    new Review
    {
        reviewId = "86",
        reviewerEmail = "wanderlustsoul@gmail.com",
        reviewerFirstName = "Lucas",
        reviewerLastName = "Wilson",
        dateReviewed = DateTime.Now,
        content = "A philosophical and inspiring quest for one's personal legend.",
        rating = 5,
        bookId = "11",
    },
    new Review
    {
        reviewId = "87",
        reviewerEmail = "socialjustice@gmail.com",
        reviewerFirstName = "Scout",
        reviewerLastName = "Finch",
        dateReviewed = DateTime.Now,
        content = "A timeless classic that addresses important societal issues.",
        rating = 5,
        bookId = "12",
    },
    new Review
    {
        reviewId = "88",
        reviewerEmail = "empathyadvocate@gmail.com",
        reviewerFirstName = "Atticus",
        reviewerLastName = "Finch",
        dateReviewed = DateTime.Now,
        content = "An impactful narrative advocating for empathy and justice.",
        rating = 5,
        bookId = "12",
    },
    new Review
    {
        reviewId = "89",
        reviewerEmail = "inspiringjustice@gmail.com",
        reviewerFirstName = "Harper",
        reviewerLastName = "Lee",
        dateReviewed = DateTime.Now,
        content = "A profound exploration of racial injustice and moral growth.",
        rating = 4,
        bookId = "12",
    },
    new Review
    {
        reviewId = "90",
        reviewerEmail = "equalityadvocate@gmail.com",
        reviewerFirstName = "Jem",
        reviewerLastName = "Finch",
        dateReviewed = DateTime.Now,
        content = "A poignant story teaching valuable lessons about equality.",
        rating = 4,
        bookId = "12",
    },
    new Review
    {
        reviewId = "91",
        reviewerEmail = "compassionateheart@gmail.com",
        reviewerFirstName = "Dill",
        reviewerLastName = "Harris",
        dateReviewed = DateTime.Now,
        content = "A heartwarming tale that teaches compassion and integrity.",
        rating = 5,
        bookId = "12",
    },
    new Review
    {
        reviewId = "92",
        reviewerEmail = "dystopianfan@gmail.com",
        reviewerFirstName = "Winston",
        reviewerLastName = "Smith",
        dateReviewed = DateTime.Now,
        content = "A chilling and thought-provoking glimpse into a totalitarian world.",
        rating = 5,
        bookId = "13",
    },
    new Review
    {
        reviewId = "93",
        reviewerEmail = "societycritic@gmail.com",
        reviewerFirstName = "Julia",
        reviewerLastName = "Brown",
        dateReviewed = DateTime.Now,
        content = "An unsettling yet compelling commentary on power and control.",
        rating = 4,
        bookId = "13",
    },
    new Review
    {
        reviewId = "94",
        reviewerEmail = "thoughtprovoker@gmail.com",
        reviewerFirstName = "George",
        reviewerLastName = "Orwell",
        dateReviewed = DateTime.Now,
        content = "A haunting and prescient portrayal of a dystopian society.",
        rating = 5,
        bookId = "13",
    },
    new Review
    {
        reviewId = "95",
        reviewerEmail = "freedomadvocate@gmail.com",
        reviewerFirstName = "Edward",
        reviewerLastName = "Jones",
        dateReviewed = DateTime.Now,
        content = "A cautionary tale emphasizing the importance of individuality.",
        rating = 4,
        bookId = "13",
    },
    new Review
    {
        reviewId = "96",
        reviewerEmail = "bigbrotherwatcher@gmail.com",
        reviewerFirstName = "Sarah",
        reviewerLastName = "Evans",
        dateReviewed = DateTime.Now,
        content = "A compelling narrative warning against the dangers of surveillance.",
        rating = 5,
        bookId = "13",
    },
    new Review
    {
        reviewId = "97",
        reviewerEmail = "youthfulangst@gmail.com",
        reviewerFirstName = "Holden",
        reviewerLastName = "Caulfield",
        dateReviewed = DateTime.Now,
        content = "A raw and honest portrayal of teenage angst and disillusionment.",
        rating = 4,
        bookId = "14",
    },
    new Review
    {
        reviewId = "98",
        reviewerEmail = "lifequestioner@gmail.com",
        reviewerFirstName = "Phoebe",
        reviewerLastName = "Caulfield",
        dateReviewed = DateTime.Now,
        content = "An introspective journey through the complexities of adolescence.",
        rating = 4,
        bookId = "14",
    },
    new Review
    {
        reviewId = "99",
        reviewerEmail = "comingofage@gmail.com",
        reviewerFirstName = "Jane",
        reviewerLastName = "Gallagher",
        dateReviewed = DateTime.Now,
        content = "A narrative that captures the struggles of growing up and finding oneself.",
        rating = 4,
        bookId = "14",
    },
    new Review
    {
        reviewId = "100",
        reviewerEmail = "rebelheart@gmail.com",
        reviewerFirstName = "Sally",
        reviewerLastName = "Hayes",
        dateReviewed = DateTime.Now,
        content = "An exploration of rebellion and the search for authenticity.",
        rating = 3,
        bookId = "14",
    },
    new Review
    {
        reviewId = "101",
        reviewerEmail = "teenagestruggles@gmail.com",
        reviewerFirstName = "Robert",
        reviewerLastName = "Ackley",
        dateReviewed = DateTime.Now,
        content = "A relatable narrative portraying the challenges of adolescence.",
        rating = 4,
        bookId = "14",
    },
    new Review
    {
        reviewId = "102",
        reviewerEmail = "fantasylover@gmail.com",
        reviewerFirstName = "Bilbo",
        reviewerLastName = "Baggins",
        dateReviewed = DateTime.Now,
        content = "An adventurous and charming tale filled with magic and wonder.",
        rating = 5,
        bookId = "15",
    },
    new Review
    {
        reviewId = "103",
        reviewerEmail = "adventureseeker@gmail.com",
        reviewerFirstName = "Gandalf",
        reviewerLastName = "Grey",
        dateReviewed = DateTime.Now,
        content = "An epic journey through Middle-earth, brimming with excitement.",
        rating = 5,
        bookId = "15",
    },
    new Review
    {
        reviewId = "104",
        reviewerEmail = "fantasyenthusiast@gmail.com",
        reviewerFirstName = "Thorin",
        reviewerLastName = "Oakenshield",
        dateReviewed = DateTime.Now,
        content = "A magical quest filled with dwarves, dragons, and adventure.",
        rating = 5,
        bookId = "15",
    },
    new Review
    {
        reviewId = "105",
        reviewerEmail = "magicjourney@gmail.com",
        reviewerFirstName = "Smaug",
        reviewerLastName = "Fire-drake",
        dateReviewed = DateTime.Now,
        content = "A captivating story of courage, friendship, and treasure.",
        rating = 5,
        bookId = "15",
    },
    new Review
    {
        reviewId = "106",
        reviewerEmail = "magicaladventures@gmail.com",
        reviewerFirstName = "Bard",
        reviewerLastName = "Bowman",
        dateReviewed = DateTime.Now,
        content = "An enchanting tale of bravery and unexpected heroism.",
        rating = 5,
        bookId = "15",
    },
    new Review
    {
        reviewId = "107",
        reviewerEmail = "romancelover@gmail.com",
        reviewerFirstName = "Elizabeth",
        reviewerLastName = "Bennet",
        dateReviewed = DateTime.Now,
        content = "A delightful classic exploring love, wit, and societal expectations.",
        rating = 5,
        bookId = "16",
    },
    new Review
    {
        reviewId = "108",
        reviewerEmail = "classicnovels@gmail.com",
        reviewerFirstName = "Fitzwilliam",
        reviewerLastName = "Darcy",
        dateReviewed = DateTime.Now,
        content = "A timeless romance filled with memorable characters and wit.",
        rating = 5,
        bookId = "16",
    },
    new Review
    {
        reviewId = "109",
        reviewerEmail = "societyportrayal@gmail.com",
        reviewerFirstName = "Jane",
        reviewerLastName = "Austen",
        dateReviewed = DateTime.Now,
        content = "A charming portrayal of society and the complexities of love.",
        rating = 4,
        bookId = "16",
    },
    new Review
    {
        reviewId = "110",
        reviewerEmail = "regencyerafan@gmail.com",
        reviewerFirstName = "Charlotte",
        reviewerLastName = "Lucas",
        dateReviewed = DateTime.Now,
        content = "A story that captures the essence of the Regency era beautifully.",
        rating = 4,
        bookId = "16",
    },
    new Review
    {
        reviewId = "111",
        reviewerEmail = "classicromance@gmail.com",
        reviewerFirstName = "Mr. Collins",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A novel showcasing the complexities of love and societal norms.",
        rating = 4,
        bookId = "16",
    },
    new Review
    {
        reviewId = "112",
        reviewerEmail = "epicfantasy@gmail.com",
        reviewerFirstName = "Frodo",
        reviewerLastName = "Baggins",
        dateReviewed = DateTime.Now,
        content = "An epic fantasy that transports readers to a mesmerizing world.",
        rating = 5,
        bookId = "17",
    },
    new Review
    {
        reviewId = "113",
        reviewerEmail = "fantasyreader@gmail.com",
        reviewerFirstName = "Gandalf",
        reviewerLastName = "The Grey",
        dateReviewed = DateTime.Now,
        content = "A masterfully crafted world with rich lore and unforgettable quests.",
        rating = 5,
        bookId = "17",
    },
    new Review
    {
        reviewId = "114",
        reviewerEmail = "epicadventure@gmail.com",
        reviewerFirstName = "Aragorn",
        reviewerLastName = "Elessar",
        dateReviewed = DateTime.Now,
        content = "An immersive journey filled with bravery, battles, and triumphs.",
        rating = 5,
        bookId = "17",
    },
    new Review
    {
        reviewId = "115",
        reviewerEmail = "fantasyenthusiast@gmail.com",
        reviewerFirstName = "Legolas",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A legendary tale of friendship, heroism, and the fight against evil.",
        rating = 5,
        bookId = "17",
    },
    new Review
    {
        reviewId = "116",
        reviewerEmail = "middleearthfan@gmail.com",
        reviewerFirstName = "Samwise",
        reviewerLastName = "Gamgee",
        dateReviewed = DateTime.Now,
        content = "An unforgettable odyssey through Middle-earth's vast landscapes.",
        rating = 5,
        bookId = "17",
    },
    new Review
    {
        reviewId = "117",
        reviewerEmail = "historicalfiction@gmail.com",
        reviewerFirstName = "Liesel",
        reviewerLastName = "Meminger",
        dateReviewed = DateTime.Now,
        content = "A heartbreaking yet beautiful story set during a tumultuous time.",
        rating = 5,
        bookId = "18",
    },
    new Review
    {
        reviewId = "118",
        reviewerEmail = "ww2history@gmail.com",
        reviewerFirstName = "Max",
        reviewerLastName = "Vandenburg",
        dateReviewed = DateTime.Now,
        content = "An emotionally powerful narrative from a unique perspective.",
        rating = 5,
        bookId = "18",
    },
    new Review
    {
        reviewId = "119",
        reviewerEmail = "emotionalread@gmail.com",
        reviewerFirstName = "Rudy",
        reviewerLastName = "Steiner",
        dateReviewed = DateTime.Now,
        content = "A poignant and captivating tale of courage and humanity.",
        rating = 4,
        bookId = "18",
    },
    new Review
    {
        reviewId = "120",
        reviewerEmail = "narrativegem@gmail.com",
        reviewerFirstName = "Death",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A beautifully written and haunting story narrated by Death.",
        rating = 4,
        bookId = "18",
    },
    new Review
    {
        reviewId = "121",
        reviewerEmail = "heartrendingstory@gmail.com",
        reviewerFirstName = "Hans",
        reviewerLastName = "Hubermann",
        dateReviewed = DateTime.Now,
        content = "An emotional journey through the eyes of resilient characters.",
        rating = 5,
        bookId = "18",
    },
    new Review
    {
        reviewId = "122",
        reviewerEmail = "emotionalreads@gmail.com",
        reviewerFirstName = "Amir",
        reviewerLastName = "Khan",
        dateReviewed = DateTime.Now,
        content = "A poignant and powerful story of guilt, redemption, and friendship.",
        rating = 5,
        bookId = "19",
    },
    new Review
    {
        reviewId = "123",
        reviewerEmail = "friendshipjourney@gmail.com",
        reviewerFirstName = "Hassan",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A deeply moving narrative showcasing the strength of true friendship.",
        rating = 5,
        bookId = "19",
    },
    new Review
    {
        reviewId = "124",
        reviewerEmail = "sociopoliticalissues@gmail.com",
        reviewerFirstName = "Assef",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A confronting and thought-provoking portrayal of social issues.",
        rating = 4,
        bookId = "19",
    },
    new Review
    {
        reviewId = "125",
        reviewerEmail = "redemptionstory@gmail.com",
        reviewerFirstName = "Sohrab",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A gripping narrative about atonement and the search for redemption.",
        rating = 4,
        bookId = "19",
    },
    new Review
    {
        reviewId = "126",
        reviewerEmail = "powerfulnarrative@gmail.com",
        reviewerFirstName = "Rahim",
        reviewerLastName = "Khan",
        dateReviewed = DateTime.Now,
        content = "An emotionally charged story with an impactful message.",
        rating = 5,
        bookId = "19",
    },
    new Review
    {
        reviewId = "127",
        reviewerEmail = "fantasyadventures@gmail.com",
        reviewerFirstName = "Lucy",
        reviewerLastName = "Pevensie",
        dateReviewed = DateTime.Now,
        content = "An enchanting journey into a magical world filled with wonder.",
        rating = 5,
        bookId = "20",
    },
    new Review
    {
        reviewId = "128",
        reviewerEmail = "narnialover@gmail.com",
        reviewerFirstName = "Aslan",
        reviewerLastName = "",
        dateReviewed = DateTime.Now,
        content = "A timeless classic that sparks the imagination and warms the heart.",
        rating = 5,
        bookId = "20",
    },
    new Review
    {
        reviewId = "129",
        reviewerEmail = "fantasyreader@gmail.com",
        reviewerFirstName = "Edmund",
        reviewerLastName = "Pevensie",
        dateReviewed = DateTime.Now,
        content = "An adventurous and magical world that captivates both young and old.",
        rating = 5,
        bookId = "20",
    },
    new Review
    {
        reviewId = "130",
        reviewerEmail = "magicaljourney@gmail.com",
        reviewerFirstName = "Jadis",
        reviewerLastName = "The White Witch",
        dateReviewed = DateTime.Now,
        content = "A fantastical world filled with rich characters and epic adventures.",
        rating = 4,
        bookId = "20",
    },
    new Review
    {
        reviewId = "131",
        reviewerEmail = "imaginationland@gmail.com",
        reviewerFirstName = "Peter",
        reviewerLastName = "Pevensie",
        dateReviewed = DateTime.Now,
        content = "A captivating series that opens the door to a world of imagination.",
        rating = 5,
        bookId = "20",
    }
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
                    new Genre { genreId = "5", genreName = "Thriller", description = "Intense suspense and excitement" },
                    new Genre { genreId = "6", genreName = "Historical Fiction", description = "Stories set in the past with fictional elements" },
                    new Genre { genreId = "7", genreName = "Horror", description = "Eerie and frightening tales" },
                    new Genre { genreId = "8", genreName = "Adventure", description = "Exciting and daring journeys" },
                    new Genre { genreId = "9", genreName = "Dystopian", description = "Imagining oppressive and bleak societies" },
                    new Genre { genreId = "10", genreName = "Non-Fiction", description = "Factual and informative works" },
                    new Genre { genreId = "11", genreName = "Young Adult", description = "Targeted towards adolescent readers" },
                    new Genre { genreId = "12", genreName = "Historical Non-Fiction", description = "Factual accounts of historical events" },
                    new Genre { genreId = "13", genreName = "Biography", description = "Life stories of real people" },
                    new Genre { genreId = "14", genreName = "Self-Help", description = "Guides for personal improvement" },
                    new Genre { genreId = "15", genreName = "Poetry", description = "Expressive and artistic use of language" }
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
                image = "/seeder/1.jpg",
                CreatedTime = new DateTime(2023, 11, 3, 3, 39, 0)
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
                image = "/seeder/2.jpg",
                CreatedTime = new DateTime(2023, 11, 4, 5, 48, 0)
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
                image = "/seeder/3.jpg",
                CreatedTime = new DateTime(2023, 10, 25, 15, 27, 0)
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
                image = "/seeder/4.jpg",
                CreatedTime = new DateTime(2023, 10, 23, 12, 19, 0)
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
                image = "/seeder/5.jpg",
                CreatedTime = new DateTime(2023, 11, 2, 18, 15, 0)
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
                image = "/seeder/6.jpg",
                CreatedTime = new DateTime(2023, 10, 27, 19, 50, 0)
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
                image = "/seeder/7.jpg",
                CreatedTime = new DateTime(2023, 10, 30, 6, 32, 0)
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
                image = "/seeder/8.jpg",
                CreatedTime = new DateTime(2023, 10, 30, 20, 10, 0)
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
                image = "/seeder/9.jpg",
                CreatedTime = new DateTime(2023, 10, 31, 9, 43, 0)
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
                image = "/seeder/10.jpg",
                CreatedTime = new DateTime(2023, 11, 1, 18, 44, 0)
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
                image = "/seeder/11.jpg",
                CreatedTime = new DateTime(2023, 10, 28, 4, 6, 0)
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
                image = "/seeder/12.jpg",
                CreatedTime = new DateTime(2023, 10, 25, 14, 47, 0)
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
                image = "/seeder/13.jpg",
                CreatedTime = new DateTime(2023, 11, 3, 12, 22, 0)
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
                image = "/seeder/14.jpg",
                CreatedTime = new DateTime(2023, 10, 27, 4, 40, 0)
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
                image = "/seeder/15.jpg",
                CreatedTime = new DateTime(2023, 10, 24, 8, 50, 0)
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
                image = "/seeder/16.jpg",
                CreatedTime = new DateTime(2023, 11, 3, 16, 1, 0)
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
                image = "/seeder/17.jpg",
                CreatedTime = new DateTime(2023, 11, 4, 20, 12, 0)
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
                image = "/seeder/18.jpg",
                CreatedTime = new DateTime(2023, 10, 23, 23, 13, 0)
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
                image = "/seeder/19.jpg",
                CreatedTime = new DateTime(2023, 10, 25, 4, 24, 0)
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
                image = "/seeder/20.jpg",
                CreatedTime = new DateTime(2023, 11, 1, 8, 29, 0)
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
                image = "/seeder/21.jpg",
                CreatedTime = new DateTime(2023, 11, 2, 9, 38, 0)
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
                image = "/seeder/22.jpg",
                CreatedTime = new DateTime(2023, 11, 2, 14, 4, 0)
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
                image = "/seeder/23.jpg",
                CreatedTime = new DateTime(2023, 10, 29, 12, 6, 0)
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
                image = "/seeder/24.jpg",
                CreatedTime = new DateTime(2023, 10, 26, 21, 31, 0)
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
                image = "/seeder/25.jpg",
                CreatedTime = new DateTime(2023, 11, 4, 0, 58, 0)
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
                        new BookGenres { bookId = "1", genreId = "3" },  
                        new BookGenres { bookId = "2", genreId = "2" },  
                        new BookGenres { bookId = "3", genreId = "5" },  
                        new BookGenres { bookId = "4", genreId = "6" },  
                        new BookGenres { bookId = "5", genreId = "3" },  
                        new BookGenres { bookId = "6", genreId = "7" },  
                        new BookGenres { bookId = "7", genreId = "7" }, 
                        new BookGenres { bookId = "8", genreId = "3" },  
                        new BookGenres { bookId = "9", genreId = "10" }, 
                        new BookGenres { bookId = "10", genreId = "4" }, 
                        new BookGenres { bookId = "11", genreId = "9" }, 
                        new BookGenres { bookId = "12", genreId = "6" }, 
                        new BookGenres { bookId = "13", genreId = "9" }, 
                        new BookGenres { bookId = "14", genreId = "10" },
                        new BookGenres { bookId = "15", genreId = "3" },
                        new BookGenres { bookId = "16", genreId = "7" }, 
                        new BookGenres { bookId = "17", genreId = "4" },
                        new BookGenres { bookId = "18", genreId = "9" },
                        new BookGenres { bookId = "19", genreId = "3" }, 
                        new BookGenres { bookId = "20", genreId = "3" }, 
                        new BookGenres { bookId = "21", genreId = "14" },
                        new BookGenres { bookId = "22", genreId = "14" },
                        new BookGenres { bookId = "23", genreId = "6" }, 
                        new BookGenres { bookId = "24", genreId = "6" }, 
                        new BookGenres { bookId = "25", genreId = "1" }  
                );
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
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
