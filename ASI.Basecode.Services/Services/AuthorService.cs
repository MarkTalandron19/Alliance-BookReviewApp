using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
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
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        
        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddAuthor(AuthorViewModel model)
        {
            var author = new Author();
            _mapper.Map(model, author);
            _repository.AddAuthor(author);
        }

        public void DeleteAuthor(string authorId)
        {
            _repository.DeleteAuthor(authorId);
        }

        public Task<Author> GetAuthorById(string authorId)
        {
            return _repository.GetAuthorById(authorId);
        }

        public IQueryable<Author> GetAuthors()
        {
            return _repository.GetAuthors();
        }

        public void UpdateAuthor(AuthorViewModel update)
        {
            var author = new Author();
            _mapper.Map(update, author);
            _repository.UpdateAuthor(author);
        }
    }
}
