using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookInMemoryRepositry _booksInMemoryRepository;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookInMemoryRepositry bookInMemoryRepository)
        {
            _logger = logger;
            _booksInMemoryRepository = bookInMemoryRepository;
        }
        [HttpGet(nameof(GetAllBook))]
        public IEnumerable<Book> GetAllBook()
        {
            return _booksInMemoryRepository.GetAllBook();
        }
        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            return _booksInMemoryRepository.GetById(id);
        }
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Book book)
        {
            _booksInMemoryRepository.AddBook(book);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Book book)
        {
            _booksInMemoryRepository.UpdateBook(book);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Book? DeleteMethod(int id)
        {
            return _booksInMemoryRepository.DeleteBookById(id);
        }
    }
}
