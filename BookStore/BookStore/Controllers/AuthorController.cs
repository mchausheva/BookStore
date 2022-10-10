using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Middleware;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using MediatR;
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
        private readonly IMediator _mediator;
        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public async Task< IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthorRange))]
        public async Task<IActionResult> AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthors)
        {
            if (addMultipleAuthors != null && !addMultipleAuthors.AuthorRequests.Any())
                return BadRequest(addMultipleAuthors);

            var authorCollection = _mapper.Map<IEnumerable<Author>>(addMultipleAuthors.AuthorRequests);
            var result = await _mediator.Send(new AddmultipleAuthorsCommand(authorCollection));
            
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            var result = await _mediator.Send(new GetAuthorByIdCommand(id));

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddMethod))]
        public async Task<IActionResult> AddMethod([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _mediator.Send(new AddAuthorCommand(authorRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateMethod))]
        public async Task<IActionResult> UpdateMethod([FromBody] UpdateAuthorRequest authorRequest)
        {
            var result = await _mediator.Send(new UpdateAuthorCommand(authorRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(nameof(DeleteMethod))]
        public async Task<IActionResult> DeleteMethod(int id)
        {
            if (id > 0 && await _authorService.GetById(id) != null)
            {
                var result = await _mediator.Send(new DeleteAuthorCommand(id));

                return Ok(result);
            }
            return NotFound(id);
        }
    }
}
