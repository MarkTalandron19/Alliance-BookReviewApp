using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("genres")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddGenre(GenreViewModel genre)
        {
            genre.genreId = Guid.NewGuid().ToString();
            _genreService.AddGenre(genre);
            _genreService.GetGenres();
            return RedirectToAction("Index", "Genre");
        }

        [HttpGet("get")]
        public IActionResult GetGenres()
        {

            var genres = _genreService.GetGenres();
            return View(genres);
        }

        [HttpPost("update")]
        public IActionResult UpdateGenre(GenreViewModel genre)
        {
            _genreService.UpdateGenre(genre);
            return RedirectToAction("Index", "Genre");
        }

        [HttpPost("delete")]
        public IActionResult DeleteGenre(string genreId)
        {
            _genreService.DeleteGenre(genreId);
            return RedirectToAction("Index", "Genre");
        }

        [HttpGet("GetBooksWithGenres")]
        public IActionResult GetBooksWithGenre(string genreId, string genreName)
        {
            var books = _genreService.GetBooksWithGenre(genreId).ToList();

            ViewBag.GenreName = genreName;
            if (books != null)
            {

                return View("GetBooksWithGenre", books);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Index()
        {
            var genres = _genreService.GetGenres();
            return View(genres);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GenreList() 
        {
            var genres = _genreService.GetGenres();
            return View("Views/Genre/Index.cshtml", genres);
        }

        [AllowAnonymous]
        public IActionResult SignOutUser()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
