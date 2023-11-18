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
using ASI.Basecode.Data;
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
        public HomeController(IBookService bookService, IGenreService genreService,IHttpContextAccessor httpContextAccessor,
                              ILoggerFactory loggerFactory,
                              IConfiguration configuration,
                              AsiBasecodeDBContext dBContext,
                              IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _genreService = genreService;
            _bookService = bookService;
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
            const int ViewRecentBooks = 5, ViewRatingBooks = 10;
            const float AverageRatingCheck = 4.0f;

            //Get the books from Database
            List<Book> books = _dbContext.Books.ToList();

            /* Get the Top 5 recent books */

            //Sort the list by date

            List<Book> RecentBooks;

            //Only get the number of books defined by ViewRecentBooks starting from the last element

            if (books.Count >= ViewRecentBooks)
            {
                RecentBooks = new(books.TakeLast(ViewRecentBooks));
            }
            else
            {
                RecentBooks = new(books);
            }

            IComparer<Book> DateComparer = Comparer<Book>.Create((x, y) => x.CreatedTime.CompareTo(y.CreatedTime));
            RecentBooks.Sort(DateComparer);


            /* Get the Top 10 Best Rating books */

            List<BookAverageRating> TopRatedBooks = new();

            books.ForEach(book => {

                if (book.Reviews == null)
                {
                    return;
                }

                List<Review> reviews = book.Reviews.ToList();
                int Count = reviews.Count, Sum = 0;

                //Get the rating average of each book
                //If the average is greater than Average Rating Check, then the book will be added to the List of Top Rated Books

                foreach (Review review in reviews)
                {
                    Sum += review.rating;
                }

                float Avg = Sum / Count;

                if (Avg >= AverageRatingCheck)
                {
                    TopRatedBooks.Add(new(book, Avg));
                }
            });

            //For View
            if (TopRatedBooks.Count >= ViewRatingBooks)
                TopRatedBooks = TopRatedBooks.GetRange(0, ViewRatingBooks);

            HomeViewModel homeViewModel = new(RecentBooks, TopRatedBooks);

            return View(homeViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId)
        {
            var books = _bookService.GetBooks();
            var genres = await _genreService.GetGenres().ToListAsync();

            ViewData["Genres"] = genres;
            return View(books);
        }
    }
}
