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
using ASI.Basecode.WebApp.Models;
using ASI.Basecode.Services.Services;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("books")]
	[Authorize(Roles = "Bookmaster")]
	public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
		private readonly IReviewService _reviewService;
		private readonly IMapper _mapper;

        public BookController(IBookService bookService, IGenreService genreService, IReviewService reviewService, IMapper mapper)
        {
            _bookService = bookService;
            _genreService = genreService;
            _reviewService = reviewService;
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

		[HttpGet("BooksDashboard")]
		public IActionResult BooksDashboard()
		{
			var books = _bookService.GetBooks().ToList();
			var genres = _genreService.GetGenres().ToList();

			List<Book> RecentBooks = _bookService.GetRecentBooks().ToList();
			List<Book> TopRatedBooks = GetTopRatedBooks();
			List<Genre> Genres = _genreService.GetGenres().ToList();

			var viewModel = new HomeViewModel
			{
				NewlyReleasedBooks = RecentBooks,
				TopRatedBooks = TopRatedBooks,
				Genres = Genres
			};

			return View("Views/Book/BooksDashboard.cshtml", viewModel);
		}

        [HttpGet("BooksDashboardBookList")]
        public IActionResult BooksDashboardBookList(string bookId, int page = 1, int pageSize = 10, string sortBy = null)
        {
            var books = _bookService.GetBooks();
            var genres = _genreService.GetGenres().ToList();
            var reviews = _reviewService.GetReviews().ToList();

            var bookAverageRatings = new Dictionary<string, decimal>();

            foreach (var book in books)
            {
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
                if (sortBy.Equals("CreatedTime", StringComparison.OrdinalIgnoreCase))
                {
                    sortedBooksList = sortedBooksList.OrderByDescending(b => b.CreatedTime).ToList();
                }
                else if (sortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
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

        [HttpGet("getGenresOfBooks")]
        public async Task<IActionResult> GetGenresOfBook(string bookId, string image, string author, string title, string synopsis, string pubYear, string publisher, string isbn, string language)
        {
            var genres = _bookService.GetGenresOfBook(bookId).ToList();
            var reviews = _bookService.GetReviewsOfBook(bookId);

            ViewBag.image = image;
            ViewBag.title = title;
            ViewBag.author = author;
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

				if (reviews.Count <= 0)
				{
					continue;
				}

				int Count = reviews.Count;
				float Sum = 0;

				foreach (Review review in reviews)
				{
					Sum += review.rating;
				}

				double Avg = Sum / Count;
				double RoundedAvg = Math.Round(Avg, 1);

				if (RoundedAvg >= AverageRatingCheck)
				{
					TopRatedBooks.Add(new(book, RoundedAvg));
				}
			}

			IComparer<BookRating> RatingComparer = Comparer<BookRating>.Create((x, y) => y.AvgRating.CompareTo(x.AvgRating));
			TopRatedBooks.Sort(RatingComparer);

			foreach (var bookRating in TopRatedBooks)
			{
				TopBooks.Add(bookRating.Book);
			}

			if (TopBooks.Count > LimitNumberOfBooksToView)
			{
				var Top5Books = TopBooks.Take(LimitNumberOfBooksToView);
				return Top5Books.ToList();
			}

			return TopBooks;
		}
	}
}
