using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IAuthorService
    {
        IQueryable<Author> GetAuthors();
        Task<Author> GetAuthorById(string authorId);
        IQueryable<Book> GetAuthoredBooks(string authorId);
        void AddAuthor(AuthorViewModel model);
        void UpdateAuthor(AuthorViewModel update);
        void DeleteAuthor(string authorId);
    }
}
