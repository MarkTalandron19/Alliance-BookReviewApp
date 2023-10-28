using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
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
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
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

        public void AddBook(Book book, List<Author> authors, List<Genre> genres)
        {
            this.GetDbSet<Book>().Add(book);

            foreach (var author in authors)
            {
                var authoredBook = new AuthoredBooks
                {
                    bookId = book.bookId,
                    authorId = author.authorId
                };

                this.GetDbSet<AuthoredBooks>().Add(authoredBook);
            }

            foreach (var genre in genres)
            {
                var bookGenre = new BookGenres
                {
                    bookId = book.bookId,
                    genreId = genre.genreId,
                };

                this.GetDbSet<BookGenres>().Add(bookGenre);
            }

            UnitOfWork.SaveChanges();
        }

        public void DeleteBook(string bookId)
        {
            var book = this.GetDbSet<Book>().SingleOrDefault(b => b.bookId == bookId);

            if(book != null)
            {
                var authoredBooks = this.GetDbSet<AuthoredBooks>().Where(ab => ab.bookId == bookId);
                foreach (var authoredBook in authoredBooks)
                {
                    this.GetDbSet<AuthoredBooks>().Remove(authoredBook);
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
                book.synopsis = update.synopsis;
                book.pubYear = update.pubYear;
                UnitOfWork.SaveChanges();
            }
        }

        public bool BookExists(string bookId)
        {
            return this.GetDbSet<Book>().Any(x => x.bookId == bookId);
        }
    }
}
