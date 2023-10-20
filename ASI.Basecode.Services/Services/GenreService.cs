using ASI.Basecode.Data.Interfaces;
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
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreService, IMapper mapper)
        {
            _repository = genreService;
            _mapper = mapper;
        }

        public void AddGenre(Genre genre)
        {
            _repository.AddGenre(genre);
        }

        public void DeleteGenre(string genreId)
        {
            _repository.DeleteGenre(genreId);
        }

        public IQueryable<Genre> GetGenres()
        {
            return _repository.GetGenres();
        }

        public void UpdateGenre(Genre update)
        {
            _repository.UpdateGenre(update);
        }
    }
}
