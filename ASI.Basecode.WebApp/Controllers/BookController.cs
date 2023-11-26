using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("books")]
	[Authorize(Roles = "Bookmaster")]
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
        public IActionResult AddBook(BookViewModel book)
        {
            book.bookId = Guid.NewGuid().ToString();
            _bookService.AddBook(book);
            return RedirectToAction("BookList", "Book");
        }

        [HttpGet("get")]
        public IActionResult GetBooks()
        {
            var books =_bookService.GetBooks();
            return RedirectToAction("BookList", "Book");
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

        [HttpPost("update")]
        public IActionResult UpdateBook(BookViewModel book)
        {
            _bookService.UpdateBook(book);
            return RedirectToAction("BookList", "Book");
        }

        [HttpPost("delete")]
        public IActionResult DeleteBook(string bookId)
        {
            _bookService.DeleteBook(bookId);
            return RedirectToAction("BookList", "Book");
        }

        [HttpGet]
        public IActionResult BookList()
        {
            var books = _bookService.GetBooks();
            return View(books);
        }

        [HttpGet("getGenresOfBooks")]
        public IActionResult GetGenresOfBook(string bookId, string title, string synopsis, string pubYear, string publisher, string isbn, string language)
        {
            var genres = _bookService.GetGenresOfBook(bookId).ToList();

            ViewBag.title = title;
            ViewBag.synopsis = synopsis;
            ViewBag.pubYear = pubYear;
            ViewBag.publisher = publisher;
            ViewBag.isbn = isbn;
            ViewBag.language = language;

            if (genres != null)
            {

                // Return the view
                return View("GetGenresOfBook", genres);
                //return Json(new { Genres = genres });
            }
            else
            {
                // Handle the case where the book with the given ID was not found.
                return NotFound();
            }
        }

        [AllowAnonymous]
        public IActionResult SignOutUser()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpGet("BookDetail/{bookId}")]
        public async Task<IActionResult> BookDetail(string bookId)
        {
            var book = await _bookService.GetBookById(bookId);
            var genres = _bookService.GetGenresOfBook(bookId);

            ViewData["Genres"] = await genres.ToListAsync();
            return View(book);
        }
    }
}
