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
		private readonly AsiBasecodeDBContext _dbContext;
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
            _dbContext = dBContext;
        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            const int NumOfViewRecentBooks = 5;

			//Get the books from Database
			List<Book> books = _dbContext.Books.ToList();

			List<Book> RecentBooks = GetRecentBooks(books, NumOfViewRecentBooks);

            HomeViewModel homeViewModel = new(RecentBooks);


			return View(homeViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId)
        {
            var books = _bookService.GetBooks();
            var genres = await _genreService.GetGenres().ToListAsync();
            var reviews = await _reviewService.GetReviews().ToListAsync();

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;
            return View(books);
        }

		private static List<Book> GetRecentBooks(List<Book> books, int? NumOfViewRecentBooks = null)
		{
			//Sort the list by date

			List<Book> RecentBooks;

			//Only get the number of books defined by ViewRecentBooks starting from the last element

			if (NumOfViewRecentBooks != null && books.Count >= NumOfViewRecentBooks)
			{
				RecentBooks = new(books.TakeLast(NumOfViewRecentBooks.Value));
			}
			else
			{
				RecentBooks = new(books);
			}

			IComparer<Book> DateComparer = Comparer<Book>.Create((x, y) => x.CreatedTime.CompareTo(y.CreatedTime));
			RecentBooks.Sort(DateComparer);
			return RecentBooks;
		}
	}
}
