using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet(nameof(GetAllAuthors))]
        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorService.GetAllAuthors();
        }
        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            return _authorService.GetById(id);
        }
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Author author)
        {
            _authorService.AddAuthor(author);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Author author)
        {
            _authorService.UpdateAuthor(author);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Author? DeleteMethod(int id)
        {
            return _authorService.DeleteAuthorById(id);
        }
    }
}
