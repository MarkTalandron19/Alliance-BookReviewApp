using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
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
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddBook(BookViewModel model, List<Genre> genres)
        {
            var book = new Book();
            if(!_repository.BookExists(model.bookId))
            {
                _mapper.Map(model, book);
                book.CreatedTime = DateTime.Now;
                book.UpdatedTime = DateTime.Now;
                book.CreatedBy = System.Environment.UserName;
                book.UpdatedBy = System.Environment.UserName;
                _repository.AddBook(book, genres);
            }
        }

        public void DeleteBook(string bookId)
        {
            if(_repository.BookExists(bookId))
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

        public IQueryable<Genre> GetGenresOfBook(string bookId)
        {
            return _repository.GetGenresOfBook(bookId);
        }

        public void UpdateBook(BookViewModel update)
        {
            var book = new Book();
            if(_repository.BookExists(update.bookId))
            {
                _mapper.Map(update, book);
                book.UpdatedTime = DateTime.Now;
                book.UpdatedBy = System.Environment.UserName;
                _repository.UpdateBook(book);
            }
        }
    }
}
