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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("delete")]
        public IActionResult DeleteGenre(string genreId)
        {
            _genreService.DeleteGenre(genreId);
            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GenreList() 
        {
            return View();
        }
    }
}
