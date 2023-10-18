using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{

    [Route("authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        
        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddAuthor(Author author) 
        {
            _authorService.AddAuthor(author);
            return CreatedAtAction("GetAuthorById", new { authorId = author.authorId }, author);
        }

        [HttpGet("get")]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("get/{authorId}")]

        public async Task<IActionResult> GetAuthorById(string authorId)
        {
            var author = await _authorService.GetAuthorById(authorId);

            if(author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPut("update")]
        public IActionResult UpdateAuthor(Author author)
        {
            _authorService.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("delete/{authorId}")]
        public IActionResult DeleteAuthor(string authorId)
        {
            _authorService?.DeleteAuthor(authorId);
            return NoContent();
        }
    }
}
