using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
        public HomeController(IBookService bookService, IGenreService genreService, IReviewService reviewService, IHttpContextAccessor httpContextAccessor,
                              ILoggerFactory loggerFactory,
                              IConfiguration configuration,
                              IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _genreService = genreService;
            _bookService = bookService;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId)
        {
            var books = _bookService.GetBooks().ToList();
            var genres = _genreService.GetGenres().ToList();
            var reviews = _reviewService.GetReviews().ToList();

            foreach (var book in books)
            {
                book.BookGenres = AssignGenresToBook(book);
            }

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;
            return View(books);
        }

        private List<BookGenres> AssignGenresToBook(Book book)
        {
            List<Genre> genres = _bookService.GetGenresOfBook(book.bookId).ToList();
            List<BookGenres> bookGenres = new();

            foreach (var genre in genres)
            {
                BookGenres genresOfThisBook = new()
                {
                    genreId = genre.genreId,
                    genre = genre,
                    bookId = book.bookId,
                   
                };
                bookGenres.Add(genresOfThisBook);
            }

            return bookGenres;
        }
    }
}
