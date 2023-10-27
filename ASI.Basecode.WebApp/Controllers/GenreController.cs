using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public IActionResult UpdateGenre(GenreViewModel genre)
        {
            _genreService.UpdateGenre(genre);
            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public IActionResult DeleteGenre(string genreId)
        {
            _genreService.DeleteGenre(genreId);
            return RedirectToAction("Index", "Home");
        }
    }
}
