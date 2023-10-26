using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        public GenreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IQueryable<Genre> GetGenres()
        {
            return this.GetDbSet<Genre>();
        }

        public void AddGenre(Genre genre)
        {
            this.GetDbSet<Genre>().Add(genre);
            UnitOfWork.SaveChanges();
        }

        public void DeleteGenre(string genreId)
        {
            var genre = this.GetDbSet<Genre>().SingleOrDefault(g => g.genreId == genreId);

            if (genre != null)
            {
                this.GetDbSet<Genre>().Remove(genre);
                UnitOfWork.SaveChanges();
            }
        }

        public void UpdateGenre(Genre update)
        {
            var genre = this.GetDbSet<Genre>().SingleOrDefault(g => g.genreId == update.genreId);

            if (genre != null)
            {
                genre.genreName = update.genreName;
                genre.description = update.description;
                UnitOfWork.SaveChanges();
            }
        }

        public bool GenreExists(string genreId)
        {
            return this.GetDbSet<Genre>().Any(x => x.genreId == genreId);
        }
    }
}
