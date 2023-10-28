using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IGenreService
    {
        IQueryable<Genre> GetGenres();
        IQueryable<Book> GetBooksWithGenre(string genreId);
        void AddGenre(GenreViewModel model);
        void UpdateGenre(GenreViewModel update);
        void DeleteGenre(string genreId);
    }
}
