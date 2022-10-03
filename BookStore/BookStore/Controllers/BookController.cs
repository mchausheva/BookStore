using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        [HttpGet(nameof(GetAllBook))]
        public IEnumerable<Book> GetAllBook()
        {
            return _bookService.GetAllBook();
        }
        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            return _bookService.GetById(id);
        }
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Book book)
        {
            _bookService.AddBook(book);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Book book)
        {
            _bookService.UpdateBook(book);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Book? DeleteMethod(int id)
        {
            return _bookService.DeleteBookById(id);
        }
    }
}
