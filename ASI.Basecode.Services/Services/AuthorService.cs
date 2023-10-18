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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        
        public AuthorService(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        public void AddAuthor(Author author)
        {
            _authorService.AddAuthor(author);
        }

        public void DeleteAuthor(string authorId)
        {
            _authorService.DeleteAuthor(authorId);
        }

        public Task<Author> GetAuthorById(string authorId)
        {
            return _authorService.GetAuthorById(authorId);
        }

        public IQueryable<Author> GetAuthors()
        {
            return _authorService.GetAuthors();
        }

        public void UpdateAuthor(Author update)
        {
            _authorService.UpdateAuthor(update);
        }
    }
}
