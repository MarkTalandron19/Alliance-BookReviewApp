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
        public IActionResult AddBook(BookViewModel book, List<Author> authors, List<Genre> genres)
        {
            book.bookId = Guid.NewGuid().ToString();
            _bookService.AddBook(book, authors, genres);
            _bookService.GetBooks();
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
        [AllowAnonymous]
        public IActionResult BookList()
        {
            var books = _bookService.GetBooks();
            return View(books);
        }

        [HttpGet("getGenresOfBook")]
        public async Task<IActionResult> GetGenresOfBook(string bookId)
        {
            var book = await _bookService.GetBookById(bookId);

            if (book != null)
            {
                var genres = book.BookGenres;
                var genreNames = genres.Select(bg => bg.genre.genreName).ToList();
                return Ok(genreNames);
            }
            else
            {
                // Handle the case where the book with the given ID was not found.
                return NotFound();
            }
        }


    }
}
