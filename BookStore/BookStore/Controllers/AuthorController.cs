using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInMemoryRepository _authorInMemoryRepository;
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(ILogger<AuthorController> logger, IAuthorInMemoryRepository authorInMemoryRepository)
        {
            _logger = logger;
            _authorInMemoryRepository = authorInMemoryRepository;
        }

        [HttpGet(nameof(GetAllAuthors))]
        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorInMemoryRepository.GetAllAuthors();
        }
        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            return _authorInMemoryRepository.GetById(id);
        }
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Author author)
        {
            _authorInMemoryRepository.AddAuthor(author);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Author author)
        {
            _authorInMemoryRepository.UpdateAuthor(author);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Author? DeleteMethod(int id)
        {
            return _authorInMemoryRepository.DeleteAuthorById(id);
        }
    }
}
