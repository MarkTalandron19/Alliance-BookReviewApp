using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _genreRepository = repository;
            _mapper = mapper;
        }

        public void AddGenre(GenreViewModel model)
        {
            var genre = new Genre();
            if(!_genreRepository.GenreExists(model.genreId))
            {
                _mapper.Map(genre, model);
                _genreRepository.AddGenre(genre);
                genre.CreatedTime = DateTime.Now;
                genre.UpdatedTime = DateTime.Now;
                genre.CreatedBy = System.Environment.UserName;
                genre.UpdatedBy = System.Environment.UserName;
            }
        }

        public void DeleteGenre(string genreId)
        {
            if(_genreRepository.GenreExists(genreId))
            {
                _genreRepository.DeleteGenre(genreId);
            }
        }

        public IQueryable<Genre> GetGenres()
        {
            return _genreRepository.GetGenres();
        }

        public void UpdateGenre(GenreViewModel update)
        {
            var genre = new Genre();
            if(_genreRepository.GenreExists(update.genreId))
            {
                _mapper.Map(genre, update);
                _genreRepository.AddGenre(genre);
                genre.UpdatedTime = DateTime.Now;
                genre.UpdatedBy = System.Environment.UserName;
            }
        }
    }
}
