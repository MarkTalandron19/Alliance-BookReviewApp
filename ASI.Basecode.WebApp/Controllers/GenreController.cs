using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AddGenre(Genre genre)
        {
            _genreService.AddGenre(genre);
            return NoContent();
        }

        [HttpGet("get")]
        public IActionResult GetGenres()
        {
            var genres = _genreService.GetGenres();
            return Ok(genres);
        }

        [HttpPut("update/{genreId}")]
        public IActionResult UpdateGenre(Genre genre)
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
    }
}
