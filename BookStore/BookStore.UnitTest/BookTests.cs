using AutoMapper;
using BookStore.AutoMapper;
using BookStore.BL.Services;
using BookStore.Controllers;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.UnitTest
{
    public class BookTests
    {
        private List<Book> _book = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "Title Book",
                AuthorId = 1,
                LastUpdated = DateTime.Now,
                Price = 10
            },
            new Book()
            {
                Id = 2,
                Title = "Book Title",
                AuthorId = 2,
                LastUpdated = DateTime.Now,
                Price = 15
            }
        };
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<BookService>> _loggerMock;
        private readonly Mock<ILogger<BookController>> _loggerBookControllerMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        public BookTests()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();

            _loggerMock = new Mock<ILogger<BookService>>();
            _loggerBookControllerMock = new Mock<ILogger<BookController>>();
            _bookRepositoryMock = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Book_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;
            _bookRepositoryMock.Setup(x => x.GetAllBooks())
                                 .ReturnsAsync(_book);

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.GetAllBooks();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var books = okObjectResult.Value as IEnumerable<Book>;
            Assert.NotNull(books);
            Assert.NotEmpty(books);
            Assert.Equal(expectedCount, books.Count());
            Assert.Equal(books, _book);
        }
        [Fact]
        public async Task Book_GetBookById_Ok()
        {
            //setup 
            var bookId = 1;
            _bookRepositoryMock.Setup(x => x.GetById(bookId))
                                 .ReturnsAsync(_book.First(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.GetById(bookId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var book = okObjectResult.Value as Book;
            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }
        [Fact]
        public async Task Book_GetBookById_NotOk()
        {
            //setup 
            var bookId = 3;
            _bookRepositoryMock.Setup(x => x.GetById(bookId))!
                                 .ReturnsAsync(_book.FirstOrDefault(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.GetById(bookId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedBookId = (int)notFoundObjectResult.Value;
            Assert.Equal(bookId, returnedBookId);
        }
        [Fact]
        public async Task Book_AddBook_Ok()
        {
            //setup 
            var bookRequest = new AddBookRequest()
            {
                Id = 1,
                Title = "Title Book",
                AuthorId = 1,
                LastUpdated = DateTime.Now,
                Price = 10
            };
            var expectedBookId = 3;
            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>()))
                                 .Callback(() =>
                                 {
                                     _book.Add(new Book()
                                     {
                                         Id = expectedBookId,
                                         Title = bookRequest.Title,
                                         AuthorId = bookRequest.AuthorId,
                                         LastUpdated = bookRequest.LastUpdated,
                                         Price = bookRequest.Price
                                     });
                                 })!.ReturnsAsync(() => _book.FirstOrDefault(x => x.Id == expectedBookId));
            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.AddMethod(bookRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddBookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedBookId, resultValue.Book.Id);
            Assert.Equal("Successfully Added Book", resultValue.Message);

        }
        [Fact]
        public async Task Book_AddExistedBook_NotOk()
        {
            //setup 
            var bookRequest = new AddBookRequest()
            {
                Id = 1,
                Title = "Title Book",
                AuthorId = 1,
                LastUpdated = DateTime.Now,
                Price = 10
            };
            var expectedBookId = 3;
            _bookRepositoryMock.Setup(x => x.GetById(bookRequest.Id))!
                               .ReturnsAsync(() => _book.FirstOrDefault(x => x.Id == bookRequest.Id));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.AddMethod(bookRequest);

            // Assert
            var objectResult = result as BadRequestObjectResult;
            Assert.NotNull(objectResult);

            var resultValue = objectResult.Value as AddBookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("This Book Already Exist!", resultValue.Message);
        }
        [Fact]
        public async Task Book_UpdateBook_Ok()
        {
            //setup 
            var bookRequest = new UpdateBookRequest()
            {
                Id = 1,
                Title = "Title Book",
                AuthorId = 1,
                LastUpdated = DateTime.Now,
                Price = 10
            };

            _bookRepositoryMock.Setup(x => x.GetById(bookRequest.Id))!
                               .ReturnsAsync(() => _book.FirstOrDefault(x => x.Id == bookRequest.Id));
            

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.UpdateMethod(bookRequest);
            //Assert
            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var resultValue = objectResult.Value as UpdateBookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Successfully Updated Book", resultValue.Message);

        }
        [Fact]
        public async Task Book_UpdateBok_NotOk()
        {
            //setup 
            var bookRequest = new UpdateBookRequest()
            {
                Id = 3,
                Title = "Title Book",
                AuthorId = 1,
                LastUpdated = DateTime.Now,
                Price = 10
            };

            _bookRepositoryMock.Setup(x => x.GetById(bookRequest.Id))!
                               .ReturnsAsync(_book.FirstOrDefault(x => x.Id == bookRequest.Id));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.UpdateMethod(bookRequest);

            //Assert
            var objectResult = result as BadRequestObjectResult;
            Assert.NotNull(objectResult);

            var resultValue = objectResult.Value as UpdateBookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Not Updated", resultValue.Message);
        }
        [Fact]
        public async Task Book_DeleteBook_Ok()
        {
            //setup 
            var bookId = 1;
            _bookRepositoryMock.Setup(x => x.GetById(bookId))!
                               .ReturnsAsync(_book.FirstOrDefault(x => x.Id == bookId));
            _bookRepositoryMock.Setup(x => x.DeleteBookById(bookId))
                               .Callback(() =>
                               {
                                   _book.RemoveAll(x => x.Id == bookId);
                               })!.ReturnsAsync(_book.FirstOrDefault(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.DeleteMethod(bookId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var book = okObjectResult.Value as Book;
            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }
        [Fact]
        public async Task Book_DeleteBook_NotOk()
        {
            //setup 
            var bookId = 1;
            _bookRepositoryMock.Setup(x => x.DeleteBookById(bookId))
                               .ReturnsAsync(_book.First(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.DeleteMethod(bookId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedBookId = (int)notFoundObjectResult.Value;
            Assert.Equal(bookId, returnedBookId);
        }
    }
}
