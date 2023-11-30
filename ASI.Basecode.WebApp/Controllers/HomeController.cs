﻿using ASI.Basecode.Services.Interfaces;
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
using System.Collections.Generic;
using ASI.Basecode.Data.Models;
using System.Data.Entity;
using ASI.Basecode.Data;
using Newtonsoft.Json;
using System;
using ASI.Basecode.WebApp.Models;
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
        public async Task<IActionResult> Home()
        {
            //await _genreService.GetGenres().ToListAsync();
            //await SetupYears()
            List<Genre> genres = _dbContext.Genres.ToList();
            List<int> years = SetupYears();

            IComparer<Genre> GenreSorter = Comparer<Genre>.Create((x, y) => x.genreName.CompareTo(y.genreName));
            genres.Sort(GenreSorter);

            HomeViewModel homeViewModel = new() 
            { 
                Genres = genres,
                Years = years
            };

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

        [HttpGet]
        public IActionResult Search(string SearchText, string Year, string Genre)
        {
            List<Book> AllBooks = _dbContext.Books.ToList();
            List<Book> SearchResults = new();

            if(SearchText == null)
            {
                return RedirectToAction("Home");
            }

            foreach(Book book in AllBooks)
            {
                //Check if the book's title contains of the given search text

                book.title = book.title.ToLower();

                if (!book.title.Contains(SearchText)) continue;

                if (Year != null)
                {
                    //Check if the book's created year matches to the given year
                    if (book.CreatedTime.Year != int.Parse(Year)) continue;
                }

                book.BookGenres ??= AssignGenresToBook(book);

                if (Genre != null)
                {
                    var BookGenres = book.BookGenres;

                    //Check if the given genre is found or exits in this current book's collection of BookGenre

                    bool IsBookHaveAnyOfGivenGenre = false;

                    foreach(var bookGenre in BookGenres)
                    {
                        string givenGenre = Genre.ToLower(), genreOftheBook = bookGenre.genre.genreName.ToLower();

                        if(genreOftheBook.Contains(givenGenre))
                        {
                            IsBookHaveAnyOfGivenGenre= true;
                            break;
                        }
                    }

                    if (IsBookHaveAnyOfGivenGenre == false) continue;
                }

                SearchResults.Add(book);
            }

            string SearchResultJson = JsonConvert.SerializeObject(SearchResults, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            HttpContext.Session.SetString("SearchResult", SearchResultJson);
            return RedirectToAction("Home");
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
    }
}
