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
    public class AuthorTests
    {
        private List<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Age = 74,
                DateOfBirth = DateTime.Now,
                Name = "Author Name",
                Nickname = "Nickname"
            },
            new Author()
            {
                Id = 2,
                Age = 46,
                DateOfBirth = DateTime.Now,
                Name = "Another Name",
                Nickname = "Another Nickname"
            }
        };
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AuthorService>> _loggerMock;
        private readonly Mock<ILogger<AuthorController>> _loggerAuthorControllerMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        //private readonly Mock<IBookRepositry> _bookRepositoryMock;

        public AuthorTests()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();

            _loggerMock = new Mock<ILogger<AuthorService>>();
            _loggerAuthorControllerMock = new Mock<ILogger<AuthorController>>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            //_bookRepositoryMock = new Mock<IBookRepositry>();
        }
        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;
            _authorRepositoryMock.Setup(x => x.GetAllAuthors())
                                 .ReturnsAsync(_authors);
            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);
            
            //act
            var result = await controller.GetAllAuthors();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
            Assert.Equal(authors, _authors);
        }
        [Fact]
        public async Task Author_GetAuthorById_Ok()
        {
            //setup 
            var authorId = 1;
            _authorRepositoryMock.Setup(x => x.GetById(authorId))
                                 .ReturnsAsync(_authors.First(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }
        [Fact]
        public async Task Author_GetAuthorById_NotOk()
        {
            //setup 
            var authorId = 3;
            _authorRepositoryMock.Setup(x => x.GetById(authorId))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedAuthorId = (int)notFoundObjectResult.Value;
            Assert.Equal(authorId, returnedAuthorId);
        }
        [Fact]
        public async Task Author_AddAuthor_Ok()
        {
            //setup 
            var authorRequest = new AddAuthorRequest()
            {
                Name = "New Name",
                Nickname = "New Nickname",
                Age = 22,
                DateOfBirth = DateTime.Now
            };
            var expectedAuthorId = 3;
            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>()))
                                 .Callback(() =>
                                 {
                                     _authors.Add(new Author()
                                     {
                                         Id = expectedAuthorId,
                                         Name = authorRequest.Name,
                                         Age = authorRequest.Age,
                                         DateOfBirth = authorRequest.DateOfBirth,
                                         Nickname = authorRequest.Nickname
                                     });
                                 })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == expectedAuthorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.AddMethod(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resutValue = okObjectResult.Value as AddAuthorResponse;
            Assert.NotNull(resutValue);
            Assert.Equal(expectedAuthorId, resutValue.Author.Id);
        }
        [Fact]
        public async Task Author_AddExistedAuthor_NotOk()
        {
            //setup 
            var authorRequest = new AddAuthorRequest()
            {
                Id = 1,
                Age = 74,
                DateOfBirth = DateTime.Now,
                Name = "Author Name",
                Nickname = "Nickname"
            };

            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.AddMethod(authorRequest);

            //Assert
            var objectResult = result as BadRequestObjectResult;
            Assert.NotNull(objectResult);

            var resutValue = objectResult.Value as AddAuthorResponse;
            Assert.NotNull(resutValue);
            Assert.Equal("Author Already Exist!", resutValue.Message);
        }
        [Fact]
        public async Task Author_UpdateAuthor_Ok()
        {
            //setup 
            var authorRequest = new UpdateAuthorRequest()
            {
                Id = 1,
                Age = 74,
                DateOfBirth = DateTime.Now,
                Name = "Author Name",
                Nickname = "Nickname"
            };

            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));
            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.UpdateMethod(authorRequest);

            //Assert
            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var resultValue = objectResult.Value as UpdateAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Successfully Updated Author", resultValue.Message);

        }
        [Fact]
        public async Task Author_UpdateAuthor_NotOk()
        {
            //setup 
            var authorRequest = new UpdateAuthorRequest()
            {
                Id = 1,
                Age = 75,
                DateOfBirth = DateTime.Now,
                Name = "Author Name New",
                Nickname = "Nickname New"
            };

            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));
            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);
            //act
            var result = await controller.UpdateMethod(authorRequest);

            //Assert
            var objectResult = result as BadRequestObjectResult;
            Assert.NotNull(objectResult);

            var resultValue = objectResult.Value as UpdateAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Not Updated", resultValue.Message);
        }
        [Fact]
        public async Task Author_DeleteAuthor_Ok()
        {
            //setup
            var authorId = 1;
            _authorRepositoryMock.Setup(x => x.DeleteAuthorById(authorId))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.DeleteMethod(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Author;
            Assert.NotNull(resultValue);
        }
        [Fact]
        public async Task Author_DeleteAuthor_NotOk()
        {
            //setup
            var authorId = 3;
            _authorRepositoryMock.Setup(x => x.DeleteAuthorById(authorId))!
                                 .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.DeleteMethod(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedAuthorId = (int)notFoundObjectResult.Value;
            Assert.Equal(authorId, returnedAuthorId);
        }
    }
}