using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<Book> GetBooks();
        Task<Book> GetBookById(string bookId);
        void AddBook(Book book, List<Author> authors, List<Genre> genres);
        void UpdateBook(Book update);
        void DeleteBook(string bookId);
        bool BookExists(string bookId);
    }
}
