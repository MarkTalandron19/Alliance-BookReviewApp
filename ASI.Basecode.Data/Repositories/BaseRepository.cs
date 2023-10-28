using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Basecode.Data.Repositories
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected AsiBasecodeDBContext Context => (AsiBasecodeDBContext)UnitOfWork.Database;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {

            using (var asd = new AsiBasecodeDBContext())
            {
                var firstTable = asd.Genres;
                var secondTable = asd.Books;

                var query = asd.Books
    .Join(asd.Book_Genres, book => book.bookId, bg => bg.bookId, (book, bg) => new { book, bg })
    .Join(asd.Genres, joinedData => joinedData.bg.genreId, genre => genre.genreId, (joinedData, genre) => new { joinedData.book, genre })
    .Where(joinedData => joinedData.book.bookId == joinedData.genre.genreId)
    .Select(joinedData => new
    {
        Book = joinedData.book,
        Genre = joinedData.genre,
    })
    .ToList();
            }
            return Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }
    }
}
