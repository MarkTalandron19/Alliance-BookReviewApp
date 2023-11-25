using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IGenreRepository genreRepository, IMapper mapper)
        {
            _repository = repository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public void AddBook(BookViewModel model)
        {
            var book = new Book();
            if (!_repository.BookExists(model.bookId))
            {
                _mapper.Map(model, book);
                book.CreatedTime = DateTime.Now;
                book.UpdatedTime = DateTime.Now;
                book.CreatedBy = System.Environment.UserName;
                book.UpdatedBy = System.Environment.UserName;

                // Handle genre
                var genreName = model.genre!;

                // Check if the genre already exists
                var existingGenre = _genreRepository.GetGenres().FirstOrDefault(g => g.genreName == genreName);

                if (existingGenre == null)
                {
                    // Genre doesn't exist, create a new one
                    var newGenre = new Genre { genreName = genreName };
                    _genreRepository.AddGenre(newGenre);

                    // Ensure that BookGenres is initialized
                    if (book.BookGenres == null)
                    {
                        book.BookGenres = new List<BookGenres>();
                    }

                    // Create BookGenres relationship
                    book.BookGenres.Add(new BookGenres { genre = newGenre, bookId = book.bookId, genreId = newGenre.genreId });
                }
                else
                {
                    // Ensure that BookGenres is initialized
                    if (book.BookGenres == null)
                    {
                        book.BookGenres = new List<BookGenres>();
                    }

                    // Genre already exists, use existing genre
                    book.BookGenres.Add(new BookGenres { genre = existingGenre, bookId = book.bookId, genreId = existingGenre.genreId });
                }

                // Handle image upload
                if (model.image != null && model.image.Length > 0)
                {
                    // Define the directory to store the uploaded image
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // Get the file extension
                    var fileExtension = Path.GetExtension(model.image.FileName);

                    // Extract only alphanumeric characters from the book title
                    var cleanedBookName = Regex.Replace(model.title, @"[^A-Za-z0-9]+", "");

                    // Generate a timestamp in YY-MM-DD-Time format
                    var timestamp = DateTime.Now.ToString("yy-MM-dd-HHmmssfff");

                    // Combine the book name, timestamp, and file extension
                    var uniqueFileName = $"{cleanedBookName}_{timestamp}{fileExtension}";

                    // Define the path where the image will be stored
                    var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

                    // Save the image to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.image.CopyTo(fileStream);
                    }

                    // Store the file path in the Book model's image property
                    book.image = "/uploads/" + uniqueFileName; // Adjust the path as per your project's structure
                }




                _repository.AddBook(book);
            }
        }


        public IQueryable<Genre> GetGenresOfBook(string bookId)
        {
            // Implement the logic to retrieve genres of a book from your repository
            return _repository.GetGenresOfBook(bookId);
        }

        public IQueryable<Review> GetReviewsOfBook(string bookId)
        {
            // Implement the logic to retrieve genres of a book from your repository
            return _repository.GetReviewsOfBook(bookId);
        }


        public void DeleteBook(string bookId)
        {
            if (_repository.BookExists(bookId))
            {
                _repository.DeleteBook(bookId);
            }
        }

        public async Task<Book> GetBookById(string bookId)
        {
            return await _repository.GetBookById(bookId);
        }

        public IQueryable<Book> GetBooks()
        {
            return _repository.GetBooks();
        }

        public void UpdateBook(BookViewModel book)
        {
            // Retrieve the existing book entity
            var existingBook = _repository.GetBookById(book.bookId).Result;

            if (existingBook != null)
            {
                Console.WriteLine($"Existing Book: {existingBook.title}");

                // Map the properties from the book parameter to the existing book
                _mapper.Map(book, existingBook);

                // Update the time and user information
                existingBook.UpdatedTime = DateTime.Now;
                existingBook.UpdatedBy = System.Environment.UserName;

                // Ensure that BookGenres is initialized
                if (existingBook.BookGenres == null)
                {
                    existingBook.BookGenres = new List<BookGenres>();
                    Console.WriteLine("BookGenres is null, initialized as a new list.");
                }
                else
                {
                    // Clear existing BookGenres
                    existingBook.BookGenres.Clear();
                    Console.WriteLine("Cleared existing BookGenres.");
                }

                // Remove existing BookGenres entries related to the book
                _repository.RemoveBookGenresForBook(existingBook.bookId);

                // Handle genre
                var genreName = book.genre!;

                // Check if the genre already exists
                var existingGenre = _genreRepository.GetGenres().FirstOrDefault(g => g.genreName == genreName);

                if (existingGenre == null)
                {
                    Console.WriteLine($"Genre '{genreName}' doesn't exist, creating a new one.");

                    // Genre doesn't exist, create a new one
                    var newGenre = new Genre { genreName = genreName };
                    _genreRepository.AddGenre(newGenre);

                    // Create BookGenres relationship
                    existingBook.BookGenres.Add(new BookGenres { genre = newGenre, bookId = existingBook.bookId, genreId = newGenre.genreId });
                }
                else
                {
                    Console.WriteLine($"Genre '{genreName}' already exists, using existing genre.");

                    // Genre already exists, use existing genre
                    existingBook.BookGenres.Add(new BookGenres { genre = existingGenre, bookId = existingBook.bookId, genreId = existingGenre.genreId });
                }

                // Handle image upload
                if (book.image != null && book.image.Length > 0)
                {
                    // Delete the current image if it exists
                    if (!string.IsNullOrEmpty(existingBook.image))
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.image.TrimStart('/'));

                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                            Console.WriteLine("Existing image deleted.");
                        }
                    }

                    // Define the directory to store the uploaded image
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // Get the file extension
                    var fileExtension = Path.GetExtension(book.image.FileName);

                    // Extract only alphanumeric characters from the book title
                    var cleanedBookName = Regex.Replace(book.title, @"[^A-Za-z0-9]+", "");

                    // Generate a timestamp in YY-MM-DD-Time format
                    var timestamp = DateTime.Now.ToString("yy-MM-dd-HHmmssfff");

                    // Combine the book name, timestamp, and file extension
                    var uniqueFileName = $"{cleanedBookName}_{timestamp}{fileExtension}";

                    // Define the path where the image will be stored
                    var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

                    // Save the image to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        book.image.CopyTo(fileStream);
                    }

                    // Store the file path in the Book model's image property
                    existingBook.image = "/uploads/" + uniqueFileName; // Adjust the path as per your project's structure
                }

                // Update the book in the repository
                _repository.UpdateBook(existingBook);

                Console.WriteLine("Book updated successfully!");

            }


        }
    }
}
