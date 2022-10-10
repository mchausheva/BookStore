using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;
        public BookController(ILogger<BookController> logger, IBookService bookService, IMediator mediator)
        {
            _logger = logger;
            _bookService = bookService;
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllBooks))]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            var result = await _mediator.Send(new GetBookByIdCommand(id));

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddMethod))]
        public async Task<IActionResult> AddMethod([FromBody] AddBookRequest bookRequest)
        {
            var result = await _mediator.Send(new AddBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateMethod))]
        public async Task<IActionResult> UpdateMethod([FromBody] UpdateBookRequest bookRequest)
        {
            var result = await _mediator.Send(new UpdateBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(nameof(DeleteMethod))]
        public async Task<IActionResult> DeleteMethod([FromBody] int id)
        {
            if (id > 0 && await _bookService.GetById(id) != null)
            {
                var result = await _mediator.Send(new DeleteBookCommand(id));
                return Ok(result);
            }
            return NotFound(id);
        }
    }
}
