using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookRepository(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : base(unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IQueryable<Book> GetBooks()
        {
            return this.GetDbSet<Book>();
        }

        public Task<Book> GetBookById(string bookId)
        {
            var book = this.GetDbSet<Book>().Where(b => b.bookId == bookId).SingleOrDefaultAsync();

            return book;
        }

        public IQueryable<Genre> GetGenresOfBook(string bookId)
        {
            var bookGenres = this.GetDbSet<BookGenres>()
                .Include(bg => bg.genre)
                .Where(bg => bg.bookId == bookId)
                .Select(bg => bg.genre);

            return bookGenres;
        }

        public IQueryable<Review> GetReviewsOfBook(string bookId)
        {
            var bookReviews = this.GetDbSet<Review>()
                .Where(bg => bg.bookId == bookId);

            return bookReviews;
        }

        public void AddBook(Book book)
        {
            this.GetDbSet<Book>().Add(book);

            UnitOfWork.SaveChanges();
        }

        public void DeleteBook(string bookId)
        {
            var book = this.GetDbSet<Book>().SingleOrDefault(b => b.bookId == bookId);

            if(book != null)
            {
                var bookGenres = this.GetDbSet<BookGenres>().Where(bg => bg.bookId == bookId);
                foreach (var bookGenre in bookGenres)
                {
                    this.GetDbSet<BookGenres>().Remove(bookGenre);
                }
                this.GetDbSet<Book>().Remove(book);
                UnitOfWork.SaveChanges();
            }
        }

        public void UpdateBook(Book update)
        {
           var book = this.GetDbSet<Book>().SingleOrDefault(b => b.bookId == update.bookId);

            if (book != null)
            {
                book.title = update.title;
                book.synopsis = update.synopsis;
                book.pubYear = update.pubYear;
                book.publisher = update.publisher;
                book.isbn = update.isbn;
                book.language = update.language;
                UnitOfWork.SaveChanges();
            }
        }

        public bool BookExists(string bookId)
        {
            return this.GetDbSet<Book>().Any(x => x.bookId == bookId);
        }

        public void RemoveBookGenresForBook(string bookId)
        {
            var bookGenres = this.GetDbSet<BookGenres>().Where(bg => bg.bookId == bookId);
            foreach (var bookGenre in bookGenres)
            {
                this.GetDbSet<BookGenres>().Remove(bookGenre);
            }
            UnitOfWork.SaveChanges();
        }

		public IQueryable<Book> GetRecentBooks()
		{
			return this.GetDbSet<Book>().OrderByDescending(b => b.CreatedTime).Take(5);
		}
	}
}
