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
using ASI.Basecode.Data;
using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.Linq;
using ASI.Basecode.WebApp.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using Microsoft.IdentityModel.Tokens;

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

        private const int _numOfViewRecentBooks = 5;
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
                              AsiBasecodeDBContext dBContext,
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
        public async Task<IActionResult> Home()
        {
			//Get the books from Database
			//List<Book> books = await _bookService.GetBooks().ToListAsync();

			List<Book> RecentBooks = _bookService.GetRecentBooks().ToList();

            var viewModel = new HomeViewModel
            {
                NewlyReleasedBooks = RecentBooks
            };

			return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId, int page = 1, int pageSize = 10, string genreId = null)
        {
            var books = _bookService.GetBooks();
            var genres = await _genreService.GetGenres().ToListAsync();
            var reviews = await _reviewService.GetReviews().ToListAsync();

            if (!string.IsNullOrEmpty(genreId))
            {
                books = books.Where(b => b.BookGenres.Any(bg => bg.genreId == genreId));
            }

            var paginatedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)books.Count() / pageSize);

            ViewBag.Page = page;

            return View(paginatedBooks);
        }

        [HttpGet]
        public IActionResult BookDetail(string bookId) => RedirectToAction("BookDetail", "Book", new { bookId });

        /*private static List<Book> GetRecentBooks(List<Book> books)
		{
			//Sort the list by date

			List<Book> RecentBooks;

			//Only get the number of books defined by ViewRecentBooks starting from the last element

			if (books.Count >= _numOfViewRecentBooks)
			{
				RecentBooks = new(books.TakeLast(_numOfViewRecentBooks));
			}
			else
			{
				RecentBooks = new(books);
			}

			IComparer<Book> DateComparer = Comparer<Book>.Create((x, y) => y.CreatedTime.CompareTo(x.CreatedTime));
			RecentBooks.Sort(DateComparer);
			return RecentBooks;
		}*/
	}
}
