using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepositry _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        public BookService(IBookRepositry bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public AddBookResponse AddBook(AddBookRequest bookRequest)
        {
            try
            {
                var bookExist = _bookRepository.GetByTitle(bookRequest.Title);
                if (bookExist != null)
                    return new AddBookResponse()
                    {
                        Book = bookExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "This Book Already Exist!"
                    };

                var book = _mapper.Map<Book>(bookExist);
                var result = _bookRepository.AddBook(book);

                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result,
                    Message = "Successfully Added Book"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The book can not be add");
                throw;
            }
        }

        public Book DeleteBookById(int id)
        {
            return _bookRepository.DeleteBookById(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            _logger.LogInformation("Success");
            return _bookRepository.GetAllBooks();
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public UpdateBookResponse UpdateBook(UpdateBookRequest bookRequest)
        {
            try
            {
                var bookExist = _bookRepository.GetById(bookRequest.Id);
                if (bookExist == null)
                    return new UpdateBookResponse()
                    {
                        Book = bookExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };

                var book = _mapper.Map<Book>(bookExist);
                var result = _bookRepository.UpdateBook(book);

                return new UpdateBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result,
                    Message = "Successfully Updated Book"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The book can not be update");
                throw;
            }
        }
    }
}
