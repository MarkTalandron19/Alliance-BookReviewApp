﻿using ASI.Basecode.Data.Models;
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
        void AddAuthor(Author author);
        void UpdateAuthor(Author update);
        void DeleteAuthor(string authorId);
    }
}
