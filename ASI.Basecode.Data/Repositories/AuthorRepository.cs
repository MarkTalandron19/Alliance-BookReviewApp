﻿using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Author> GetAuthors()
        {
            return this.GetDbSet<Author>();
        }

        public Task<Author> GetAuthorById(string authorId)
        {
            var author = this.GetDbSet<Author>().Where(a => a.authorId == authorId).SingleOrDefaultAsync();

            return author;
        }

        public void AddAuthor(Author author)
        {
            this.GetDbSet<Author>().Add(author);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAuthor(string authorId)
        {
            var author = this.GetDbSet<Author>().SingleOrDefault(a => a.authorId == authorId);

            if (author != null)
            {
                this.GetDbSet<Author>().Remove(author);
                UnitOfWork.SaveChanges();
            }
        }

        public void UpdateAuthor(Author update)
        {
            var author = this.GetDbSet<Author>().SingleOrDefault(a => a.authorId == update.authorId);

            if (author != null)
            {
                author.authorFirstName = update.authorFirstName;
                author.authorLastName = update.authorLastName;
                UnitOfWork.SaveChanges();
            }
        }
    }
}
