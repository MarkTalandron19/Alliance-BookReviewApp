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
using System.Data.Entity;

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
            List<Book> TopRatedBooks = GetTopRatedBooks();

            var viewModel = new HomeViewModel
            {
                NewlyReleasedBooks = RecentBooks,
                TopRatedBooks = TopRatedBooks
            };

			return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId)
        {
            var books = _bookService.GetBooks();
            var genres = _genreService.GetGenres().ToList();
            var reviews = _reviewService.GetReviews().ToList();

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;
            return View(books);
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

        private List<Book> GetTopRatedBooks()
        {
            const float AverageRatingCheck = 4.0f;
            const int LimitNumberOfBooksToView = 5;

            List<Book> AllBooks = _bookService.GetBooks().ToList(), TopBooks = new();

            foreach (var book in AllBooks)
            {
                List<Review> reviews = _reviewService.GetBookReview(book.bookId).ToList();

                if(reviews.Count <= 0)
                {
                    continue;
                }

                int Count = reviews.Count, Sum = 0;

                //Get the rating average of each book
                //If the average is greater than Average Rating Check, then the book will be added to the List of Top Rated Books

                foreach (Review review in reviews)
                {
                    Sum += review.rating;
                }

                float Avg = Sum / Count;

                if(Avg >= AverageRatingCheck)
                {
                    TopBooks.Add(book);
                }
            }

            if(TopBooks.Count >  LimitNumberOfBooksToView)
            {
                return TopBooks.GetRange(0, LimitNumberOfBooksToView);
            }

            return TopBooks;
        }
    }
}
