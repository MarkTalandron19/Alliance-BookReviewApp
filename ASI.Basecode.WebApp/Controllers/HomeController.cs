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
using System;
using Newtonsoft.Json;
using System.Globalization;

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
            List<Genre> Genres = _genreService.GetGenres().ToList();
            List<int> Years = SetupYears();

            var viewModel = new HomeViewModel
            {
                NewlyReleasedBooks = RecentBooks,
                TopRatedBooks = TopRatedBooks,
                Genres = Genres,
                Years = Years

            };

            try
            {
                if (HttpContext.Session != null && HttpContext.Session.GetString("SearchResult") != null)
                {
                    viewModel.SearchResults = JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("SearchResult"));
                }
            }
            catch
            {
                viewModel.SearchResults = null;
            }


            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Search(string SearchText, string Year, string Genre)
        {
            List<Book> AllBooks = _bookService.GetBooks().ToList();
            List<Book> SearchResults = new();

            if (SearchText == null)
            {
                return RedirectToAction("Home");
            }

            foreach (Book book in AllBooks)
            {
                //Check if the book's title contains of the given search text

                book.title = book.title.ToLower();

                if (!book.title.Contains(SearchText.ToLower())) continue;

                SearchResults.Add(book);
            }

            string SearchResultJson = JsonConvert.SerializeObject(SearchResults, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            HttpContext.Session.SetString("SearchResult", SearchResultJson);
            return RedirectToAction("Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Library(string bookId, int page = 1, int pageSize = 10, string sortBy = null)
        {
            var books = _bookService.GetBooks();
            var genres = _genreService.GetGenres().ToList();
            var reviews = _reviewService.GetReviews().ToList();

            var bookAverageRatings = new Dictionary<string, decimal>();

            foreach (var book in books)
             {
                book.BookGenres = AssignGenresToBook(book);

                var bookReviews = reviews.Where(r => r.bookId == book.bookId).ToList();
                if (bookReviews.Any())
                {
                    decimal totalRatings = bookReviews.Sum(r => r.rating);
                    decimal averageRating = totalRatings / bookReviews.Count;
                    bookAverageRatings[book.bookId] = Math.Round(averageRating, 1);
                }
                else
                {
                    bookAverageRatings[book.bookId] = 0;
                }
            }

            var sortedBooksList = books.ToList();


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    sortedBooksList = sortedBooksList.OrderBy(b => b.title).ToList();
                }
                else if (sortBy.Equals("rating", StringComparison.OrdinalIgnoreCase))
                {
                    sortedBooksList = sortedBooksList.OrderByDescending(b => bookAverageRatings.ContainsKey(b.bookId) ? bookAverageRatings[b.bookId] : 0).ToList();
                }
            }


            var paginatedBooks = sortedBooksList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)sortedBooksList.Count() / pageSize);
            ViewData["SortBy"] = sortBy;


            ViewBag.Page = page;

            return View(paginatedBooks);
        }

        [HttpGet]
        public IActionResult Search(string SearchText, string Year, string Genre)
        {
            List<Book> AllBooks = _bookService.GetBooks().ToList();
            List<Book> SearchResults = new();

            if (SearchText == null)
            {
                return RedirectToAction("Home");
            }

            foreach (Book book in AllBooks)
            {
                //Check if the book's title contains of the given search text

                book.title = book.title.ToLower();

                if (!book.title.Contains(SearchText.ToLower())) continue;

                SearchResults.Add(book);
            }

            string SearchResultJson = JsonConvert.SerializeObject(SearchResults, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            HttpContext.Session.SetString("SearchResult", SearchResultJson);
            return RedirectToAction("Home");
        }

        public static List<Book> FilterSearchResultWithYear(List<Book> SearchResults, string Year)
        {
            List<Book> FilteredSearchResults = new();

            foreach (var book in SearchResults)
            {
                //Check if the book's created year matches to the given year
                if (book.CreatedTime.Year != int.Parse(Year)) continue;
                FilteredSearchResults.Add(book);
            }

            return FilteredSearchResults;
        }

        public List<Book> FilterSearchResultWithGenre(List<Book> SearchResults, string Genre)
        {
            List<Book> FilteredSearchResult = new();

            foreach (var book in SearchResults)
            {
                List<Genre> genresOfThisBook = _bookService.GetGenresOfBook(book.bookId).ToList();

                bool IsBookHaveAnyOfGivenGenre = false;
                foreach (var genre in genresOfThisBook)
                {
                    string genreName = genre.genreName.ToLower();

                    if (genreName.Contains(Genre.ToLower()))
                    {
                        IsBookHaveAnyOfGivenGenre = true;
                        break;
                    }
                }

                if (IsBookHaveAnyOfGivenGenre == false) continue;

               FilteredSearchResult.Add(book);
            }

            return FilteredSearchResult;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult FilterByGenre(string searchTerm, string selectedGenre, int page = 1, int pageSize = 10, string sortBy = null)
        {
            var allBooks = _bookService.GetBooks();

            var filteredBooks = allBooks
                .Where(book =>
                    string.IsNullOrEmpty(searchTerm) ||
                    book.title.ToLower().Contains(searchTerm) ||
                    book.publisher.ToLower().Contains(searchTerm) ||
                    book.BookGenres.Any(genre => genre.genre.genreName.ToLower().Contains(searchTerm.ToLower()))
                );

            if (!string.IsNullOrEmpty(selectedGenre))
            {
                 filteredBooks = allBooks.Where(book => book.BookGenres.Any(genre => genre.genre.genreId.Equals(selectedGenre)));
            }
            var sortedBooksList = filteredBooks.OrderBy(b => b.title);

            var paginatedBooks = sortedBooksList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var genres = _genreService.GetGenres().ToList();
            var reviews = _reviewService.GetReviews().ToList();

            ViewData["Genres"] = genres;
            ViewData["Reviews"] = reviews;

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)sortedBooksList.Count() / pageSize);
            ViewData["SortBy"] = sortBy;

            ViewBag.Page = page;

            ViewData["SelectedGenre"] = selectedGenre;
            ViewData["SearchTerm"] = searchTerm;

            return View("Library", paginatedBooks);
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

        struct BookRating
        {
            public BookRating(Book Book, double AvgRating)
            {
                this.AvgRating = AvgRating;
                this.Book = Book;
            }

            public Book Book { get; set; }  
            public double AvgRating { get; set; }
        }

        private List<Book> GetTopRatedBooks()
        {
            const float AverageRatingCheck = 4.5f;
            const int LimitNumberOfBooksToView = 5;

            List<Book> AllBooks = _bookService.GetBooks().ToList(), TopBooks = new();
            List<BookRating> TopRatedBooks = new();

            foreach (var book in AllBooks)
            {
                List<Review> reviews = _reviewService.GetBookReview(book.bookId).ToList();

                if(reviews.Count <= 0)
                {
                    continue;
                }

                int Count = reviews.Count;
                float Sum = 0;

                //Get the rating average of each book
                //If the average is greater than Average Rating Check, then the book will be added to the List of Top Rated Books

                foreach (Review review in reviews)
                {
                    Sum += review.rating;
                }

                double Avg = Sum / Count;
                double RoundedAvg = Math.Round(Avg, 1);

                if(RoundedAvg >= AverageRatingCheck)
                {
                    TopRatedBooks.Add(new(book, RoundedAvg));
                }
            }

            IComparer<BookRating> RatingComparer = Comparer<BookRating>.Create((x,y) => y.AvgRating.CompareTo(x.AvgRating));
            TopRatedBooks.Sort(RatingComparer);

            foreach (var bookRating in TopRatedBooks)
            {
                TopBooks.Add(bookRating.Book);
            }

            if(TopBooks.Count >  LimitNumberOfBooksToView)
            {
                var Top5Books = TopBooks.Take(LimitNumberOfBooksToView);
                return Top5Books.ToList();
            }

            return TopBooks;
        }

        private List<int> SetupYears()
        {
            List<Book> books = _bookService.GetBooks().ToList();
            int minYear = DateTime.Now.Year;

            List<int> years = new() { minYear };

            foreach (Book book in books)
            {
                if (minYear < book.CreatedTime.Year)
                {
                    minYear = book.CreatedTime.Year;
                    years.Add(book.CreatedTime.Year);
                }
            }

            return years;
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
                    bookId = book.bookId
                };
                bookGenres.Add(genresOfThisBook);
            }

            return bookGenres;
        }


    }
} 
