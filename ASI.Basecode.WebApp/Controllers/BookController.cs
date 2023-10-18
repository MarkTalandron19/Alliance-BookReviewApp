using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddBook(Book book, List<Author> authors, List<Genre> genres)
        {
            _bookService.AddBook(book, authors, genres);
            return NoContent();
        }

        [HttpGet("get")]
        public IActionResult GetBooks()
        {
            var books =_bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("get/{bookId}")]
        public async Task<IActionResult> GetBookById(string bookId)
        {
            var book = await _bookService.GetBookById(bookId);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("update")]
        public IActionResult UpdateBook(Book book)
        {
            _bookService.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("delete/{bookId}")]
        public IActionResult DeleteBook(string bookId)
        {
            _bookService.DeleteBook(bookId);
            return NoContent();
        }
    }
}
