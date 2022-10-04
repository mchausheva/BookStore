using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public IActionResult GetAllAuthors()
        {

            return Ok(_authorService.GetAllAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthorRange))]
        public IActionResult AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthors)
        {
            if (addMultipleAuthors != null && !addMultipleAuthors.AuthorRequests.Any())
                return BadRequest(addMultipleAuthors);

            var authorCollection = _mapper.Map<IEnumerable<Author>>(addMultipleAuthors.AuthorRequests);
            var result = _authorService.AddMultipleAuthors(authorCollection);

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            var result = _authorService.GetById(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddMethod))]
        public IActionResult AddMethod([FromBody] AddAuthorRequest authorRequest)
        {
            //if (authorRequest == null) return BadRequest(authorRequest);
            //var authorExist = _authorService.GetAuthorByName(authorRequest.Name);
            //if (authorExist != null) return BadRequest("Author Already Exist!");

            var result = _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateMethod))]
        public IActionResult UpdateMethod([FromBody] UpdateAuthorRequest authorRequest)
        {
            var result = _authorService.UpdateAuthor(authorRequest);
            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteMethod))]
        public IActionResult DeleteMethod(int id)
        {
            if (id > 0 && _authorService.GetById != null)
            {
                _authorService.DeleteAuthorById(id);
                return Ok($"Author with id {id} is successfully deleted");
            }
            return BadRequest($"Author with id {id} is not deleted");
        }
    }
}
