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
using System.Collections;
using System;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

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

        private const float _averageRatingCheck = 4.0f;
        private const int _weekLimit = 2;

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
            _dbContext = dBContext;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>
        /// 

        [HttpGet]
        public IActionResult Search(string SearchText, string Year, string Genre)
        {
            List<Book> AllBooks = _dbContext.Books.ToList();
            List<Book> SearchResults = new();

            if(SearchText == null)
            {
                return RedirectToAction("Home", "Home", SearchResults);
            }

            foreach (Book book in AllBooks)
            {
                //Check if the book's title contains of the given search text

                book.title = book.title.ToLower();

                if (!book.title.Contains(SearchText)) continue;

                if(Year != null)
                {
                    //Check if the book's created year matches to the given year
                    if (book.CreatedTime.Year != int.Parse(Year)) continue;
                }

                book.BookGenres ??= AssignGenresToBook(book);

                if (Genre != null)
                {
                    var BookGenres = book.BookGenres.ToList();

                    //Check if the given genre is found or exits in this current book's collection of BookGenre

                    bool IsBookHaveAnyOfGivenGenre = BookGenres.Any((genre) =>
                    {
                        foreach(var bookGenre in BookGenres)
                        {
                            if (genre.genre.genreName.ToLower().Contains(bookGenre.genre.genreName.ToLower()))
                            {
                                return true;
                            }
                        }
                        return false;
                    });

                    if (IsBookHaveAnyOfGivenGenre == false) continue;
                }

                SearchResults.Add(book);
            }

            //TODO: need to know how to view the search result
            string SearchResultJson = JsonConvert.SerializeObject(SearchResults, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            HttpContext.Session.SetString("SearchResult",SearchResultJson);
            return RedirectToAction("Home", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            const int NumOfViewRecentBooks = 5, NumOfViewRatingBooks = 10;
            

            //Get the books from Database
            List<Book> books = _dbContext.Books.ToList();
            List<Genre> genres = _dbContext.Genres.ToList();
            List<int> years = SetupYears();


            List<Book> RecentBooks = GetRecentBooks(books, NumOfViewRecentBooks);
            List<BookAverageRating> TopRatedBooks = GetTopRatedBooks(books);

            if (TopRatedBooks.Count >= NumOfViewRatingBooks)
                TopRatedBooks = TopRatedBooks.GetRange(0, NumOfViewRatingBooks);

            IComparer<Genre> GenreSorter = Comparer<Genre>.Create((x, y) => x.genreName.CompareTo(y.genreName));
            genres.Sort(GenreSorter);

            HomeViewModel homeViewModel = new(RecentBooks, TopRatedBooks, genres, years);
            try
            {
                if (HttpContext.Session != null && HttpContext.Session.GetString("SearchResult") != null)
                {
                    homeViewModel.SearchResults = JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("SearchResult"));
                }
            }
            catch
            {
                homeViewModel.SearchResults = null;
            }

            return View(homeViewModel);

        }

        public IActionResult RecentBooksPages()
        {
            List<Book> AllRecentBooks = GetRecentBooks(_dbContext.Books.ToList());
            List<Book> LimitedRecentBooksForView = new();
            DateTime MinDateToView = GetDateSubtractedByWeekLimit();

            foreach (var book in AllRecentBooks)
            {
                if (DateTime.Compare(book.CreatedTime, MinDateToView) == 0)
                {
                    break;
                }
                LimitedRecentBooksForView.Add(book);
            }

            return View(LimitedRecentBooksForView);
        }

        public IActionResult TopRatedBooksPages()
        {
            List<BookAverageRating> AllTopRatedBooks = GetTopRatedBooks(_dbContext.Books.ToList());
            List<BookAverageRating> LimitedTopRatedBooksForView = new();

            DateTime MinDateToView = GetDateSubtractedByWeekLimit();

            foreach (var book in AllTopRatedBooks)
            {
                if (DateTime.Compare(book.Book.CreatedTime, MinDateToView) == 0)
                {
                    break;
                }
                LimitedTopRatedBooksForView.Add(book);
            }

            return View(LimitedTopRatedBooksForView);
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

        private List<BookAverageRating> GetTopRatedBooks(List<Book> books)
        {
            /* Get the Top 10 Best Rating books */

            List<BookAverageRating> TopRatedBooks = new();

            books.ForEach(book =>
            {

                List<Review> reviews = AssignReviewsToBook(book);
                book.BookGenres = AssignGenresToBook(book);

                if (reviews.Count <= 0)
                {
                    return;
                }
                int Count = reviews.Count, Sum = 0;

                //Get the rating average of each book
                //If the average is greater than Average Rating Check, then the book will be added to the List of Top Rated Books

                foreach (Review review in reviews)
                {
                    Sum += review.rating;
                }

                float Avg = Sum / Count;

                if (Avg >= _averageRatingCheck)
                {
                    TopRatedBooks.Add(new(book, Avg));
                }
            });
            return TopRatedBooks;
        }

        private List<Review> AssignReviewsToBook(Book book)
        {
            List<Review> ReviewsDB = _dbContext.Reviews.ToList(), BookReviews = new();

            foreach(Review review in ReviewsDB)
            {
                if(book.bookId == review.bookId)
                {
                    BookReviews.Add(review);
                }
            }

            return BookReviews;
        }

        private List<BookGenres> AssignGenresToBook(Book book)
        {
            List<Genre> genres = _dbContext.Genres.ToList();
            List<BookGenres> bookGenres = _dbContext.Book_Genres.ToList();

            bookGenres = bookGenres.Where(bookGenre => {
                return bookGenre.bookId == book.bookId;
            }).ToList();


            return bookGenres;
        }

        private List<int> SetupYears()
        {
            List<Book> books = _dbContext.Books.ToList();
            int minYear = DateTime.Now.Year;

            List<int> years = new() { minYear };

            foreach(Book book in books)
            {
                if(minYear < book.CreatedTime.Year)
                {
                    minYear = book.CreatedTime.Year;
                    years.Add(book.CreatedTime.Year);
                }
            }

            return years;
        }

        private static DateTime GetDateSubtractedByWeekLimit()
        {
            DateTime MinDateToView = DateTime.Now.Subtract(TimeSpan.FromDays(7 * _weekLimit));
            return MinDateToView;
        }
    }
}
