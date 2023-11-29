using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASI.Basecode.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    bookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pubYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.bookId);
                });

            migrationBuilder.CreateTable(
                name: "CustomUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reviewerFirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    reviewerLastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    reviewerEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    dateReviewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId");
                });

            migrationBuilder.CreateTable(
                name: "Book_Genres",
                columns: table => new
                {
                    bookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genreId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Genres", x => new { x.bookId, x.genreId });
                    table.ForeignKey(
                        name: "FK_Book_Genres_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genres_Genres_genreId",
                        column: x => x.genreId,
                        principalTable: "Genres",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e5b4ff19-cc2c-4e46-86e9-8b702eb16526", 0, "36df5ec9-2cc3-4158-b692-72af7ca2e3f7", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEA5o3n+R+SpQsDR1B5S6sLXyyzH1nGUYnmXVB6P7771KqYzr+gBIi2FRAn3u+2Pmjw==", null, false, "50ee2c87-8adb-4912-a0e3-3f9e1ac1da19", false, "admin" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "bookId", "CreatedBy", "CreatedTime", "UpdatedBy", "UpdatedTime", "author", "image", "isbn", "language", "pubYear", "publisher", "synopsis", "title" },
                values: new object[,]
                {
                    { "1", null, new DateTime(2023, 11, 3, 3, 39, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ransom Riggs", "/seeder/1.jpg", "ISBN-9781594746031", "English", "2011", "Quirk Books", "When Jacob discovers clues to a mystery that spans different worlds and times, he finds a magical place known as Miss Peregrine's Home for Peculiar Children.", "Miss Peregrine's Home for Peculiar Children" },
                    { "10", null, new DateTime(2023, 11, 1, 18, 44, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F. Scott Fitzgerald", "/seeder/10.jpg", "ISBN-9780743273565", "English", "1925", "Charles Scribner's Sons", "Jay Gatsby's extravagant parties and his pursuit of Daisy Buchanan, set against the backdrop of 1920s Long Island, ultimately leading to tragic consequences.", "The Great Gatsby" },
                    { "11", null, new DateTime(2023, 10, 28, 4, 6, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paulo Coelho", "/seeder/11.jpg", "ISBN-9780062315007", "English", "1988", "HarperCollins", "Follows the journey of Santiago, an Andalusian shepherd boy, as he travels in search of a worldly treasure.", "The Alchemist" },
                    { "12", null, new DateTime(2023, 10, 25, 14, 47, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harper Lee", "/seeder/12.jpg", "ISBN-9780061120084", "English", "1960", "J. B. Lippincott & Co.", "A classic novel set in the American South during the 1930s, dealing with the issues of racial injustice and moral growth through the eyes of Scout Finch.", "To Kill a Mockingbird" },
                    { "13", null, new DateTime(2023, 11, 3, 12, 22, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "George Orwell", "/seeder/13.jpg", "ISBN-9780451524935", "English", "1949", "Secker & Warburg", "George Orwell's dystopian novel explores a totalitarian society controlled by a single party led by Big Brother.", "1984" },
                    { "14", null, new DateTime(2023, 10, 27, 4, 40, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.D. Salinger", "/seeder/14.jpg", "ISBN-9780316769488", "English", "1951", "Little, Brown and Company", "Narrates the experiences of Holden Caulfield, a disenchanted teenager navigating life and dealing with societal phoniness.", "The Catcher in the Rye" },
                    { "15", null, new DateTime(2023, 10, 24, 8, 50, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R. Tolkien", "/seeder/15.jpg", "ISBN-9780261102217", "English", "1937", "Allen & Unwin", "The story of Bilbo Baggins, a hobbit who embarks on an adventurous journey to help a group of dwarves reclaim their homeland.", "The Hobbit" },
                    { "16", null, new DateTime(2023, 11, 3, 16, 1, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rhiannon Frater", "/seeder/16.jpg", "ISBN-9780765331267", "English", "2008", "Tor Books", "Katie is driving to work one beautiful day when a dead man jumps into her car and tries to eat her.  That same morning, Jenni opens a bedroom door to find her husband devouring their toddler son. ", "The First Days" },
                    { "17", null, new DateTime(2023, 11, 4, 20, 12, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Austen", "/seeder/17.jpg", "ISBN-9780141199078", "English", "1813", "T. Egerton, Whitehall", "A romantic novel revolving around Elizabeth Bennet and Mr. Darcy, exploring societal norms, love, and misunderstandings.", "Pride and Prejudice" },
                    { "18", null, new DateTime(2023, 10, 23, 23, 13, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aldous Huxley", "/seeder/18.jpg", "ISBN-9780099518471", "English", "1932", "Chatto & Windus", "A dystopian novel portraying a future society controlled by technology, conformity, and conditioning.", "Brave New World" },
                    { "19", null, new DateTime(2023, 10, 25, 4, 24, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R. Tolkien", "/seeder/19.jpg", "ISBN-9780261102361", "English", "1954", "Allen & Unwin", "A fantasy epic that chronicles the quest to destroy the One Ring and defeat the Dark Lord Sauron.", "The Lord of the Rings" },
                    { "2", null, new DateTime(2023, 11, 4, 5, 48, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ransom Riggs", "/seeder/2.jpg", "ISBN-9781594747359", "English", "2014", "Quirk Books", "Having escaped Miss Peregrine's island by the skin of their teeth, Jacob and his new friends journey to London, the peculiar capital of the world.", "Hollow City: The Second Novel of Miss Peregrine's Peculiar Children" },
                    { "20", null, new DateTime(2023, 11, 1, 8, 29, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C.S. Lewis", "/seeder/20.jpg", "ISBN-9780066238500", "English", "1950", "Geoffrey Bles", "A series of high-fantasy novels depicting the adventures in the magical land of Narnia.", "The Chronicles of Narnia" },
                    { "21", null, new DateTime(2023, 11, 2, 9, 38, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert T. Kiyosaki", "/seeder/21.jpg", "ISBN-9781612680194", "English", "1997", "Plata Publishing", "The book explores the mindset and financial knowledge needed to succeed financially, based on the author's experiences.", "Rich Dad Poor Dad" },
                    { "22", null, new DateTime(2023, 11, 2, 14, 4, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "James Clear", "/seeder/22.jpg", "ISBN-9780735211293", "English", "2018", "Avery", "A guide to creating good habits, breaking bad ones, and mastering the tiny behaviors that lead to remarkable results.", "Atomic Habits" },
                    { "23", null, new DateTime(2023, 10, 29, 12, 6, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Steven Pressfield", "/seeder/23.jpg", "ISBN-9780553580532", "English", "1998", "Bantam", "A historical novel that retells the story of the Battle of Thermopylae from the perspective of the Spartans.", "Gates of Fire" },
                    { "24", null, new DateTime(2023, 10, 26, 21, 31, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Herman Melville", "/seeder/24.jpg", "ISBN-9780142437247", "English", "1851", "Richard Bentley", "An epic saga of Captain Ahab's obsessive quest for revenge against the white whale, Moby Dick.", "Moby-Dick" },
                    { "25", null, new DateTime(2023, 11, 4, 0, 58, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Douglas Adams", "/seeder/25.jpg", "ISBN-9780345391803", "English", "1979", "Pan Books", "A comedic science fiction series following the misadventures of an unwitting human, Arthur Dent.", "The Hitchhiker's Guide to the Galaxy" },
                    { "3", null, new DateTime(2023, 10, 25, 15, 27, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ransom Riggs", "/seeder/3.jpg", "ISBN-9781594747588", "English", "2015", "Quirk Books", "A boy with extraordinary powers. An army of deadly monsters. An epic battle for the future of peculiardom.", "Library of Souls: The Third Novel of Miss Peregrine's Peculiar Children" },
                    { "4", null, new DateTime(2023, 10, 23, 12, 19, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ransom Riggs", "/seeder/4.jpg", "ISBN-9780735232143", "English", "2018", "Penguin Random House", "Having defeated the monstrous threat that nearly destroyed the peculiar world, Jacob Portman is back where his story began, in Florida.", "A Map of Days: The Fourth Novel of Miss Peregrine's Peculiar Children" },
                    { "5", null, new DateTime(2023, 11, 2, 18, 15, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ransom Riggs", "/seeder/5.jpg", "ISBN-9780735232144", "English", "2020", "Penguin Random House", "The adventure that began with Miss Peregrine's Home for Peculiar Children and continued in A Map of Days comes to a thrilling conclusion.", "The Conference of the Birds: The Fifth Novel of Miss Peregrine's Peculiar Children" },
                    { "6", null, new DateTime(2023, 10, 27, 19, 50, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alex Michaelides", "/seeder/6.jpg", "ISBN-9781250301697", "English", "2019", "Celadon Books", "Alicia Berenson's life is seemingly perfect until she shoots her husband and then never speaks another word. Theo Faber, a criminal psychotherapist, is determined to unravel her mystery.", "The Silent Patient" },
                    { "7", null, new DateTime(2023, 10, 30, 6, 32, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delia Owens", "/seeder/7.jpg", "ISBN-9780735219091", "English", "2018", "G.P. Putnam's Sons", "A mystery tale of Kya Clark, known as the Marsh Girl, accused of the murder of Chase Andrews in the quiet town of Barkley Cove.", "Where the Crawdads Sing" },
                    { "8", null, new DateTime(2023, 10, 30, 20, 10, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Erin Morgenstern", "/seeder/8.jpg", "ISBN-9780307744432", "English", "2011", "Doubleday", "Two young illusionists, Celia and Marco, are bound in a magical competition, unaware that it's a battle where only one can survive.", "The Night Circus" },
                    { "9", null, new DateTime(2023, 10, 31, 9, 43, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tara Westover", "/seeder/9.jpg", "ISBN-9780399590504", "English", "2018", "Random House", "A memoir recounting Tara Westover's inspiring journey from growing up in a strict survivalist family in Idaho to earning a Ph.D. from Cambridge University.", "Educated" }
                });

            migrationBuilder.InsertData(
                table: "CustomUsers",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "Email", "Name", "Password", "Role", "UpdatedBy", "UpdatedTime", "UserId" },
                values: new object[] { 1, "LENOVO", new DateTime(2023, 11, 29, 17, 35, 55, 804, DateTimeKind.Local).AddTicks(515), "admin@gmail.com", "admin", "Admin1!", null, "LENOVO", new DateTime(2023, 11, 29, 17, 35, 55, 804, DateTimeKind.Local).AddTicks(531), "admin" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genreId", "CreatedBy", "CreatedTime", "UpdatedBy", "UpdatedTime", "description", "genreName" },
                values: new object[,]
                {
                    { "1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exploring futuristic concepts", "Science Fiction" },
                    { "10", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Factual and informative works", "Non-Fiction" },
                    { "11", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Targeted towards adolescent readers", "Young Adult" },
                    { "12", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Factual accounts of historical events", "Historical Non-Fiction" },
                    { "13", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Life stories of real people", "Biography" },
                    { "14", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guides for personal improvement", "Self-Help" },
                    { "15", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expressive and artistic use of language", "Poetry" },
                    { "2", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intriguing puzzles and suspense", "Mystery" },
                    { "3", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Imaginary worlds and magical elements", "Fantasy" },
                    { "4", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Love and relationships", "Romance" },
                    { "5", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intense suspense and excitement", "Thriller" },
                    { "6", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stories set in the past with fictional elements", "Historical Fiction" },
                    { "7", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eerie and frightening tales", "Horror" },
                    { "8", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exciting and daring journeys", "Adventure" },
                    { "9", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Imagining oppressive and bleak societies", "Dystopian" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Superadmin", "SUPERADMIN" },
                    { "2", null, "Genremaster", "GENREMASTER" },
                    { "3", null, "Bookmaster", "BOOKMASTER" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e5b4ff19-cc2c-4e46-86e9-8b702eb16526" });

            migrationBuilder.InsertData(
                table: "Book_Genres",
                columns: new[] { "bookId", "genreId", "Id" },
                values: new object[,]
                {
                    { "1", "3", 0 },
                    { "10", "4", 0 },
                    { "11", "9", 0 },
                    { "12", "6", 0 },
                    { "13", "9", 0 },
                    { "14", "10", 0 },
                    { "15", "3", 0 },
                    { "16", "7", 0 },
                    { "17", "4", 0 },
                    { "18", "9", 0 },
                    { "19", "3", 0 },
                    { "2", "2", 0 },
                    { "20", "3", 0 },
                    { "21", "14", 0 },
                    { "22", "14", 0 },
                    { "23", "6", 0 },
                    { "24", "6", 0 },
                    { "25", "1", 0 },
                    { "3", "5", 0 },
                    { "4", "6", 0 },
                    { "5", "3", 0 },
                    { "6", "7", 0 },
                    { "7", "7", 0 },
                    { "8", "3", 0 },
                    { "9", "10", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "reviewId", "bookId", "content", "dateReviewed", "rating", "reviewerEmail", "reviewerFirstName", "reviewerLastName" },
                values: new object[,]
                {
                    { "1", "1", "Sample content", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7736), 4, "test@gmail.com", "test", "test" },
                    { "10", "1", "Enchanting world-building and a compelling storyline.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7782), 5, "avidreader@gmail.com", "Jack", "Miller" },
                    { "100", "14", "An exploration of rebellion and the search for authenticity.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8098), 3, "rebelheart@gmail.com", "Sally", "Hayes" },
                    { "101", "14", "A relatable narrative portraying the challenges of adolescence.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8100), 4, "teenagestruggles@gmail.com", "Robert", "Ackley" },
                    { "102", "15", "An adventurous and charming tale filled with magic and wonder.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8103), 5, "fantasylover@gmail.com", "Bilbo", "Baggins" },
                    { "103", "15", "An epic journey through Middle-earth, brimming with excitement.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8105), 5, "adventureseeker@gmail.com", "Gandalf", "Grey" },
                    { "104", "15", "A magical quest filled with dwarves, dragons, and adventure.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8108), 5, "fantasyenthusiast@gmail.com", "Thorin", "Oakenshield" },
                    { "105", "15", "A captivating story of courage, friendship, and treasure.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8111), 5, "magicjourney@gmail.com", "Smaug", "Fire-drake" },
                    { "106", "15", "An enchanting tale of bravery and unexpected heroism.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8113), 5, "magicaladventures@gmail.com", "Bard", "Bowman" },
                    { "107", "16", "A delightful classic exploring love, wit, and societal expectations.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8116), 5, "romancelover@gmail.com", "Elizabeth", "Bennet" },
                    { "108", "16", "A timeless romance filled with memorable characters and wit.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8118), 5, "classicnovels@gmail.com", "Fitzwilliam", "Darcy" },
                    { "109", "16", "A charming portrayal of society and the complexities of love.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8121), 4, "societyportrayal@gmail.com", "Jane", "Austen" },
                    { "11", "1", "The ending left me wanting more! Fantastic book.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7785), 5, "reader123@gmail.com", "Olivia", "Davis" },
                    { "110", "16", "A story that captures the essence of the Regency era beautifully.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8124), 4, "regencyerafan@gmail.com", "Charlotte", "Lucas" },
                    { "111", "16", "A novel showcasing the complexities of love and societal norms.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8127), 4, "classicromance@gmail.com", "Mr. Collins", "" },
                    { "112", "17", "An epic fantasy that transports readers to a mesmerizing world.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8129), 5, "epicfantasy@gmail.com", "Frodo", "Baggins" },
                    { "113", "17", "A masterfully crafted world with rich lore and unforgettable quests.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8132), 5, "fantasyreader@gmail.com", "Gandalf", "The Grey" },
                    { "114", "17", "An immersive journey filled with bravery, battles, and triumphs.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8135), 5, "epicadventure@gmail.com", "Aragorn", "Elessar" },
                    { "115", "17", "A legendary tale of friendship, heroism, and the fight against evil.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8137), 5, "fantasyenthusiast@gmail.com", "Legolas", "" },
                    { "116", "17", "An unforgettable odyssey through Middle-earth's vast landscapes.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8140), 5, "middleearthfan@gmail.com", "Samwise", "Gamgee" },
                    { "117", "18", "A heartbreaking yet beautiful story set during a tumultuous time.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8143), 5, "historicalfiction@gmail.com", "Liesel", "Meminger" },
                    { "118", "18", "An emotionally powerful narrative from a unique perspective.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8145), 5, "ww2history@gmail.com", "Max", "Vandenburg" },
                    { "119", "18", "A poignant and captivating tale of courage and humanity.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8148), 4, "emotionalread@gmail.com", "Rudy", "Steiner" },
                    { "12", "1", "A bit slow-paced but beautifully written.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7787), 4, "fictionlover@gmail.com", "Lucas", "Thompson" },
                    { "120", "18", "A beautifully written and haunting story narrated by Death.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8151), 4, "narrativegem@gmail.com", "Death", "" },
                    { "121", "18", "An emotional journey through the eyes of resilient characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8154), 5, "heartrendingstory@gmail.com", "Hans", "Hubermann" },
                    { "122", "19", "A poignant and powerful story of guilt, redemption, and friendship.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8156), 5, "emotionalreads@gmail.com", "Amir", "Khan" },
                    { "123", "19", "A deeply moving narrative showcasing the strength of true friendship.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8159), 5, "friendshipjourney@gmail.com", "Hassan", "" },
                    { "124", "19", "A confronting and thought-provoking portrayal of social issues.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8162), 4, "sociopoliticalissues@gmail.com", "Assef", "" },
                    { "125", "19", "A gripping narrative about atonement and the search for redemption.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8165), 4, "redemptionstory@gmail.com", "Sohrab", "" },
                    { "126", "19", "An emotionally charged story with an impactful message.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8168), 5, "powerfulnarrative@gmail.com", "Rahim", "Khan" },
                    { "127", "20", "An enchanting journey into a magical world filled with wonder.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8170), 5, "fantasyadventures@gmail.com", "Lucy", "Pevensie" },
                    { "128", "20", "A timeless classic that sparks the imagination and warms the heart.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8173), 5, "narnialover@gmail.com", "Aslan", "" },
                    { "129", "20", "An adventurous and magical world that captivates both young and old.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8176), 5, "fantasyreader@gmail.com", "Edmund", "Pevensie" },
                    { "13", "1", "Thrilling plot twists! Kept me guessing till the end.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7790), 5, "mysteryreader@gmail.com", "Ella", "Wilson" },
                    { "130", "20", "A fantastical world filled with rich characters and epic adventures.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8178), 4, "magicaljourney@gmail.com", "Jadis", "The White Witch" },
                    { "131", "20", "A captivating series that opens the door to a world of imagination.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8181), 5, "imaginationland@gmail.com", "Peter", "Pevensie" },
                    { "14", "1", "An adventurous journey with captivating characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7793), 4, "adventureseeker@gmail.com", "Nathan", "Adams" },
                    { "15", "1", "A good balance of mystery and fantasy elements.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7795), 4, "literaturefan@gmail.com", "Lily", "Robinson" },
                    { "16", "1", "Immersive world, though the pacing felt uneven at times.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7798), 3, "imaginative@gmail.com", "William", "Garcia" },
                    { "17", "2", "A fantastic continuation of the series! Loved the new adventures.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7801), 5, "fantasylover@gmail.com", "Sophia", "Thompson" },
                    { "18", "2", "Engaging storyline with intriguing characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7804), 4, "readerreviewer@gmail.com", "Daniel", "Roberts" },
                    { "19", "2", "The plot twists kept me on the edge of my seat!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7806), 5, "mysteryfan@gmail.com", "Eva", "Lee" },
                    { "2", "1", "Wow this is a great book!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7759), 5, "test2@gmail.com", "Aloysius", "Beronque" },
                    { "20", "2", "Immersive and well-crafted world-building.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7809), 4, "avidreader2@gmail.com", "Michael", "Anderson" },
                    { "21", "2", "Magical and adventurous! Loved every page.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7812), 5, "fantasyadventurer@gmail.com", "Ava", "Martinez" },
                    { "22", "2", "A bit slow-paced but a solid addition to the series.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7814), 3, "storybooklover@gmail.com", "Noah", "Gonzalez" },
                    { "23", "2", "Interesting twists with charming peculiarities.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7817), 4, "adventurebookfan@gmail.com", "Grace", "Hernandez" },
                    { "24", "2", "Fascinating world-building and character development.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7820), 5, "fantasyaddict@gmail.com", "Ethan", "Perez" },
                    { "25", "2", "A thrilling journey with unexpected surprises.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7822), 4, "bookworm@gmail.com", "Emma", "Flores" },
                    { "26", "2", "Good storytelling despite a slow start.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7825), 3, "fantasylover2@gmail.com", "Liam", "Wood" },
                    { "27", "3", "An epic finale to an extraordinary series!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7828), 5, "peculiarfanatic@gmail.com", "Olivia", "Evans" },
                    { "28", "3", "The adventure and suspense kept me hooked till the end.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7830), 4, "paranormalreader@gmail.com", "Jack", "Collins" },
                    { "29", "3", "A satisfying conclusion to a fantastic journey.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7833), 5, "fantasylover3@gmail.com", "Sophie", "Baker" },
                    { "3", "1", "Slow pacing, got bored of it immediately.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7762), 2, "test3@gmail.com", "Karen", "Miller" },
                    { "30", "3", "Imaginative storytelling with a fitting end.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7836), 4, "adventureseeker@gmail.com", "Lucas", "Diaz" },
                    { "31", "3", "Intriguing plot twists and intense moments.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7838), 4, "mysteryfan2@gmail.com", "Ella", "Hill" },
                    { "32", "3", "A thrilling and satisfying conclusion.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7841), 5, "fantasyadventurer2@gmail.com", "Logan", "Ward" },
                    { "33", "3", "A rollercoaster of emotions and adventures.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7843), 5, "bookworm2@gmail.com", "Mia", "Rivera" },
                    { "34", "3", "An epic journey with memorable characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7846), 5, "fantasyaddict2@gmail.com", "Aiden", "Cruz" },
                    { "35", "3", "An enjoyable ride filled with suspense.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7849), 4, "storylover@gmail.com", "Lily", "Patterson" },
                    { "36", "3", "Engaging and thrilling till the very end!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7852), 4, "fictionenthusiast@gmail.com", "Owen", "Ramirez" },
                    { "37", "4", "A captivating continuation of the peculiar series.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7854), 4, "adventurelover@gmail.com", "Ethan", "Williams" },
                    { "38", "4", "The peculiar world continues to enchant and surprise!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7857), 5, "fantasyfanatic@gmail.com", "Madison", "Harris" },
                    { "39", "4", "Riveting storytelling with new intriguing elements.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7860), 4, "peculiarenthusiast@gmail.com", "David", "Martin" },
                    { "4", "1", "Very nice Story. Impressive writing by the Author.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7765), 5, "www@gmail.com", "Aloysius", "Beronque" },
                    { "40", "4", "An engaging and peculiar journey with familiar characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7862), 4, "bookworm3@gmail.com", "Ava", "Garcia" },
                    { "41", "4", "Fascinating and full of unexpected turns!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7865), 5, "fantasyreader3@gmail.com", "Elijah", "Adams" },
                    { "42", "4", "A peculiarly delightful addition to the series.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7867), 4, "adventureseeker2@gmail.com", "Grace", "Stewart" },
                    { "43", "4", "A journey that continues to surprise and mesmerize!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7870), 5, "peculiarfan2@gmail.com", "Xavier", "Evans" },
                    { "44", "4", "An imaginative and captivating continuation.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7873), 4, "fantasylover4@gmail.com", "Luna", "Collins" },
                    { "45", "4", "Continues to immerse readers into the peculiar world!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7876), 4, "mysteryadventurer@gmail.com", "Nolan", "Diaz" },
                    { "46", "4", "Enthralling storytelling that keeps you hooked!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7878), 5, "fantasyaddict3@gmail.com", "Scarlett", "Rivera" },
                    { "47", "5", "A thrilling and fitting conclusion to the peculiar series.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7952), 5, "adventureenthusiast@gmail.com", "Noah", "Williams" },
                    { "48", "5", "An epic culmination to an extraordinary journey!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7956), 5, "fantasyfanatic2@gmail.com", "Chloe", "Harris" },
                    { "49", "5", "A satisfying and adventurous conclusion.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7959), 4, "peculiarenthusiast2@gmail.com", "Caleb", "Martin" },
                    { "5", "1", "A good read.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7768), 5, "qwerty@gmail.com", "Enrique", "Pacudan" },
                    { "50", "5", "A mesmerizing finale with unexpected twists.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7961), 5, "bookworm4@gmail.com", "Emma", "Garcia" },
                    { "51", "5", "A captivating end to an enchanting series!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7964), 5, "fantasyreader4@gmail.com", "Ethan", "Adams" },
                    { "52", "5", "A fantastic and fulfilling conclusion.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7967), 5, "adventureseeker3@gmail.com", "Zoe", "Stewart" },
                    { "53", "5", "An epic and spellbinding ending!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7970), 5, "peculiarfan3@gmail.com", "Mason", "Evans" },
                    { "54", "5", "An extraordinary finale with magical elements.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7973), 5, "fantasylover5@gmail.com", "Liam", "Collins" },
                    { "55", "5", "A fitting end to a captivating journey!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7975), 4, "mysteryadventurer2@gmail.com", "Aria", "Diaz" },
                    { "56", "5", "A delightful conclusion filled with adventure!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7978), 4, "fantasyaddict4@gmail.com", "Isabella", "Rivera" },
                    { "57", "6", "An intense psychological thriller with an unexpected twist.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7981), 5, "mysterylover@gmail.com", "Lucas", "Brown" },
                    { "58", "6", "A gripping narrative that keeps you guessing until the end.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7983), 5, "suspensefanatic@gmail.com", "Sophie", "Miller" },
                    { "59", "6", "A mind-bending thriller that leaves you in suspense.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7986), 4, "psychologyenthusiast@gmail.com", "Adam", "Wilson" },
                    { "6", "1", "Not the best out of everything, but still very good nonetheless. I slightly teared up right before the ending part.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7770), 4, "abc@gmail.com", "Aaron", "Alcuitas" },
                    { "60", "6", "An intriguing story with well-crafted characters.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7989), 4, "mysteryreader@gmail.com", "Ava", "Garcia" },
                    { "61", "6", "A suspenseful and thrilling ride from start to finish.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7992), 5, "suspenseaddict@gmail.com", "Liam", "Thompson" },
                    { "62", "7", "Beautifully written, capturing the essence of nature and emotion.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7995), 5, "naturelover@gmail.com", "Emma", "Green" },
                    { "63", "7", "A compelling narrative blending mystery and nature seamlessly.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7997), 4, "storyappreciator@gmail.com", "Noah", "Baker" },
                    { "64", "7", "An engrossing story with a vividly depicted setting.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8000), 4, "mysteryenthusiast@gmail.com", "Chloe", "Hill" },
                    { "65", "7", "Immersive storytelling that transports you to another world.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8003), 5, "booklover@gmail.com", "Mason", "Reed" },
                    { "66", "7", "A touching and atmospheric tale of nature and humanity.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8005), 5, "emotionalexperience@gmail.com", "Sophia", "Foster" },
                    { "67", "8", "An enchanting story with a magical ambiance.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8008), 5, "magicenthusiast@gmail.com", "Oliver", "Wright" },
                    { "68", "8", "Captivating and beautifully written. The magic feels real!", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8011), 5, "fantasylover@gmail.com", "Isabella", "Hughes" },
                    { "69", "8", "A mesmerizing and delightful read, perfect for fantasy lovers.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8013), 5, "whimsicalreader@gmail.com", "Gabriel", "Bell" },
                    { "7", "1", "Captivating storyline! It kept me engaged till the end.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7773), 3, "xyz@gmail.com", "Emily", "Johnson" },
                    { "70", "8", "Immersive and magical; a unique and fantastical experience.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8016), 4, "storydreamer@gmail.com", "Hannah", "Jenkins" },
                    { "71", "8", "A beautifully woven narrative with an air of mystique.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8019), 5, "enchantedtales@gmail.com", "Lucas", "Parker" },
                    { "72", "9", "An inspiring and thought-provoking memoir about resilience.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8021), 5, "inspiringjourney@gmail.com", "Ava", "Woods" },
                    { "73", "9", "A powerful story of determination and the pursuit of education.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8024), 5, "lifechangingbooks@gmail.com", "Jack", "Cooper" },
                    { "74", "9", "A moving and insightful memoir about overcoming obstacles.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8027), 4, "memoirlover@gmail.com", "Lily", "Gordon" },
                    { "75", "9", "Compelling and inspiring; a testament to the human spirit.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8030), 5, "motivatingreads@gmail.com", "Nathan", "Reyes" },
                    { "76", "9", "An incredible journey that showcases the power of education.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8032), 5, "strengthinwords@gmail.com", "Emma", "Barnes" },
                    { "77", "10", "A poignant portrayal of the Jazz Age and its disillusionment.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8035), 5, "roaring20s@gmail.com", "Leo", "Fitzgerald" },
                    { "78", "10", "A classic novel capturing the essence of a bygone era.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8038), 5, "classicslover@gmail.com", "Ella", "Thompson" },
                    { "79", "10", "A captivating story of love, wealth, and the American Dream.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8041), 4, "gatsbyfanatic@gmail.com", "Oliver", "White" },
                    { "8", "1", "An emotional rollercoaster! Loved the character development.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7776), 4, "user123@gmail.com", "David", "Smith" },
                    { "80", "10", "A profound tale of tragedy and the pursuit of happiness.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8043), 4, "literaryenthusiast@gmail.com", "Sophia", "Baker" },
                    { "81", "10", "An emblematic novel embodying the extravagance of the 1920s.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8046), 5, "readerfromthepast@gmail.com", "Jack", "Williams" },
                    { "82", "11", "A transformative journey that inspires and enlightens.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8049), 5, "dreamchasers@gmail.com", "Santiago", "Perez" },
                    { "83", "11", "A captivating tale of discovery and self-realization.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8051), 4, "adventureseeker@gmail.com", "Sophie", "Adams" },
                    { "84", "11", "An allegorical masterpiece filled with wisdom and wonder.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8055), 5, "inspirationquest@gmail.com", "Elijah", "Clark" },
                    { "85", "11", "A thought-provoking narrative about destiny and purpose.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8057), 4, "seekerofmeaning@gmail.com", "Isabella", "Lopez" },
                    { "86", "11", "A philosophical and inspiring quest for one's personal legend.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8060), 5, "wanderlustsoul@gmail.com", "Lucas", "Wilson" },
                    { "87", "12", "A timeless classic that addresses important societal issues.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8062), 5, "socialjustice@gmail.com", "Scout", "Finch" },
                    { "88", "12", "An impactful narrative advocating for empathy and justice.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8065), 5, "empathyadvocate@gmail.com", "Atticus", "Finch" },
                    { "89", "12", "A profound exploration of racial injustice and moral growth.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8068), 4, "inspiringjustice@gmail.com", "Harper", "Lee" },
                    { "9", "1", "Didn't meet my expectations, but still a decent read.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(7779), 3, "booklover@gmail.com", "Sophie", "Brown" },
                    { "90", "12", "A poignant story teaching valuable lessons about equality.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8070), 4, "equalityadvocate@gmail.com", "Jem", "Finch" },
                    { "91", "12", "A heartwarming tale that teaches compassion and integrity.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8073), 5, "compassionateheart@gmail.com", "Dill", "Harris" },
                    { "92", "13", "A chilling and thought-provoking glimpse into a totalitarian world.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8075), 5, "dystopianfan@gmail.com", "Winston", "Smith" },
                    { "93", "13", "An unsettling yet compelling commentary on power and control.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8078), 4, "societycritic@gmail.com", "Julia", "Brown" },
                    { "94", "13", "A haunting and prescient portrayal of a dystopian society.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8081), 5, "thoughtprovoker@gmail.com", "George", "Orwell" },
                    { "95", "13", "A cautionary tale emphasizing the importance of individuality.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8084), 4, "freedomadvocate@gmail.com", "Edward", "Jones" },
                    { "96", "13", "A compelling narrative warning against the dangers of surveillance.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8086), 5, "bigbrotherwatcher@gmail.com", "Sarah", "Evans" },
                    { "97", "14", "A raw and honest portrayal of teenage angst and disillusionment.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8089), 4, "youthfulangst@gmail.com", "Holden", "Caulfield" },
                    { "98", "14", "An introspective journey through the complexities of adolescence.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8092), 4, "lifequestioner@gmail.com", "Phoebe", "Caulfield" },
                    { "99", "14", "A narrative that captures the struggles of growing up and finding oneself.", new DateTime(2023, 11, 29, 17, 35, 55, 904, DateTimeKind.Local).AddTicks(8095), 4, "comingofage@gmail.com", "Jane", "Gallagher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genres_genreId",
                table: "Book_Genres",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__1788CC4D5F4A160F",
                table: "CustomUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_bookId",
                table: "Reviews",
                column: "bookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Book_Genres");

            migrationBuilder.DropTable(
                name: "CustomUsers");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
