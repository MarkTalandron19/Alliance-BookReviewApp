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

        public void AddBook(BookViewModel model, List<Author> authors, List<Genre> genres)
        {
            var book = new Book();
            _mapper.Map(model, book);
            _repository.AddBook(book, authors, genres);
        }

        public void DeleteBook(string bookId)
        {
            _repository.DeleteBook(bookId);
        }

        public async Task<Book> GetBookById(string bookId)
        {
            return await _repository.GetBookById(bookId);
        }

        public IQueryable<Book> GetBooks()
        {
            return _repository.GetBooks();
        }

        public void UpdateBook(BookViewModel update)
        {
            var book = new Book();
            _mapper.Map(update, book);
            _repository.UpdateBook(book);
        }
    }
}
