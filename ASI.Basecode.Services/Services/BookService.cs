using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookService(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public void AddBook(Book book, List<Author> authors, List<Genre> genres)
        {
            _bookService.AddBook(book, authors, genres);
        }

        public void DeleteBook(string bookId)
        {
            _bookService.DeleteBook(bookId);
        }

        public async Task<Book> GetBookById(string bookId)
        {
            return await _bookService.GetBookById(bookId);
        }

        public IQueryable<Book> GetBooks()
        {
            return _bookService.GetBooks();
        }

        public void UpdateBook(Book update)
        {
            _bookService.UpdateBook(update);
        }
    }
}
