using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreService(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        public void AddGenre(Genre genre)
        {
            _genreService.AddGenre(genre);
        }

        public void DeleteGenre(string genreId)
        {
            _genreService.DeleteGenre(genreId);
        }

        public IQueryable<Genre> GetGenres()
        {
            return _genreService.GetGenres();
        }

        public void UpdateGenre(Genre update)
        {
            _genreService.UpdateGenre(update);
        }
    }
}
