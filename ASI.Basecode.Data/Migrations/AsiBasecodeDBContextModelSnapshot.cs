﻿// <auto-generated />
using System;
using ASI.Basecode.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASI.Basecode.Data.Migrations
{
    [DbContext(typeof(AsiBasecodeDBContext))]
    partial class AsiBasecodeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ASI.Basecode.Data.Models.Author", b =>
                {
                    b.Property<string>("authorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("authorFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("authorLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("authorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.AuthoredBooks", b =>
                {
                    b.Property<string>("authorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("bookId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("authorId");

                    b.HasIndex("bookId");

                    b.ToTable("Authored_Books");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.Book", b =>
                {
                    b.Property<string>("bookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("pubYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("bookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.BookGenres", b =>
                {
                    b.Property<string>("bookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("genreId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("bookId");

                    b.HasIndex("genreId");

                    b.ToTable("Book_Genres");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.Genre", b =>
                {
                    b.Property<string>("genreId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("genreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.BookGenres", b =>
                {
                    b.Property<string>("bookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("genreId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("bookId");

                    b.HasIndex("genreId");

                    b.ToTable("Book_Genres");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.Genre", b =>
                {
                    b.Property<string>("genreId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("genreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.Review", b =>
                {
                    b.Property<string>("reviewId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("bookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("dateReviewed")
                        .HasColumnType("datetime2");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<string>("reviewerEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("reviewerFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("reviewerLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("reviewId");

                    b.HasIndex("bookId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "UQ__Users__1788CC4D5F4A160F")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.AuthoredBooks", b =>
                {
                    b.HasOne("ASI.Basecode.Data.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("authorId");

                    b.HasOne("ASI.Basecode.Data.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("bookId");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.BookGenres", b =>
                {
                    b.HasOne("ASI.Basecode.Data.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("bookId");

                    b.HasOne("ASI.Basecode.Data.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("genreId");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.BookGenres", b =>
                {
                    b.HasOne("ASI.Basecode.Data.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("bookId");

                    b.HasOne("ASI.Basecode.Data.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("genreId");
                });

            modelBuilder.Entity("ASI.Basecode.Data.Models.Review", b =>
                {
                    b.HasOne("ASI.Basecode.Data.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("bookId");
                });
#pragma warning restore 612, 618
        }
    }
}
