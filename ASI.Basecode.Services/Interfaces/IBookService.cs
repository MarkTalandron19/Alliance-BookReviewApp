using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IBookService
    {
        IQueryable<Book> GetBooks();
        Task<Book> GetBookById(string bookId);
        IQueryable<Genre> GetGenresOfBook(string bookId);
        void AddBook(BookViewModel model, List<Author> authors, List<Genre> genres);
        void UpdateBook(BookViewModel update);
        void DeleteBook(string bookId);
    }
}
