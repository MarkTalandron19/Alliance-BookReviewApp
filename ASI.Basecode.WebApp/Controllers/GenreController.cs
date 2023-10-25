using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            return NoContent();
        }

        [HttpGet("get")]
        public IActionResult GetGenres()
        {

            var genres = _genreService.GetGenres();
            return View(genres);
        }

        [HttpPut("update/{genreId}")]
        public IActionResult UpdateGenre(GenreViewModel genre)
        {
            _genreService.UpdateGenre(genre);
            return NoContent();
        }

        [HttpDelete("delete/{genreId}")]
        public IActionResult DeleteGenre(string genreId)
        {
            _genreService.DeleteGenre(genreId);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GenreList() 
        {
            return View();
        }
    }
}
