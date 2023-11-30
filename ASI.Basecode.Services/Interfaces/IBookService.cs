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
        IQueryable<Book> GetRecentBooks();
        Task<Book> GetBookById(string bookId);
        IQueryable<Genre> GetGenresOfBook(string bookId);
        IQueryable<Review> GetReviewsOfBook(string bookId);
        void AddBook(BookViewModel model);
        void UpdateBook(BookViewModel update);
        void DeleteBook(string bookId);
    }
}
