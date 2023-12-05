﻿using ASI.Basecode.Data.Models;
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
using ASI.Basecode.WebApp.Models;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("books")]
	[Authorize(Roles = "Bookmaster")]
	public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IGenreService genreService, IMapper mapper)
        {
            _bookService = bookService;
            _genreService = genreService;
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
            var books =_bookService.GetBooks().ToList();
            var genres = _genreService.GetGenres().ToList();
            var bookModel = new BookViewModel()
            {
                genres = genres
            };
            var commonViewModel = new BookViewStorageModel()
            {
                ViewModel = bookModel,
                Books = books,
            };
            //return RedirectToAction("BookList", "Book");
            return View("Views/Book/BookList.cshtml", commonViewModel);
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
            /*var books = _bookService.GetBooks();
            return View(books);*/

            var books = _bookService.GetBooks().ToList();
            var genres = _genreService.GetGenres().ToList();
            var bookModel = new BookViewModel()
            {
                genres = genres
            };
            var commonViewModel = new BookViewStorageModel()
            {
                ViewModel = bookModel,
                Books = books,
            };
            //return RedirectToAction("BookList", "Book");
            return View("Views/Book/BookList.cshtml", commonViewModel);
        }

        [HttpGet("getGenresOfBooks")]
        public async Task<IActionResult> GetGenresOfBook(string bookId, string image, string title, string synopsis, string pubYear, string publisher, string isbn, string language)
        {
            var genres = _bookService.GetGenresOfBook(bookId).ToList();
            var reviews = _bookService.GetReviewsOfBook(bookId);

            ViewBag.image = image;
            ViewBag.title = title;
            ViewBag.synopsis = synopsis;
            ViewBag.pubYear = pubYear;
            ViewBag.publisher = publisher;
            ViewBag.isbn = isbn;
            ViewBag.language = language;

            ViewData["Reviews"] = await reviews.ToListAsync();

            if (genres != null)
            {

                return View("GetGenresOfBook", genres);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        public IActionResult SignOutUser()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpGet("BookDetail")]
        [AllowAnonymous]
        public async Task<IActionResult> BookDetail(string bookId)
        {
            var book = await _bookService.GetBookById(bookId);
            var genres = _bookService.GetGenresOfBook(bookId);
            var reviews = _bookService.GetReviewsOfBook(bookId);

            ViewData["Genres"] = await genres.ToListAsync();
            ViewData["Reviews"] = await reviews.ToListAsync();

            var ratings = await reviews.ToListAsync();
            if (ratings.Count > 0)
            {
                var ratingSum = ratings.Sum(r => r.rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count;
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(book);
        }
    }
}
