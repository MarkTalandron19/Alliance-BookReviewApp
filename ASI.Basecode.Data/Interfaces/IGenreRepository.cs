using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IGenreRepository
    {
        IQueryable<Genre> GetGenres();
        IQueryable<Book> GetBooksWithGenre(string genreId);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre update);
        void DeleteGenre(string genreId);
        bool GenreExists(string genreId);
    }
}
