using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models.Users;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetContent))]
        public async Task<IActionResult> GetContent(int userId)
        {
            var result = await _shoppingCartService.GetContent(userId);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddToCart))]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCart shoppingCart)
        {
            var result = _shoppingCartService.AddToCart(shoppingCart);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(RemoveFromCart))]
        public async Task<IActionResult> RemoveFromCart([FromBody] int userId, int itemId)
        {
            var result = _shoppingCartService.RemoveFromCart(userId, itemId);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(EmptyCart))]
        public async Task<IActionResult> EmptyCart(Guid guidId)
        {
            var result = _shoppingCartService.EmptyCart(guidId);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(FinishPurchase))]
        public async Task<IActionResult> FinishPurchase(int userId)
        {
            var result = _shoppingCartService.FinishPurchase(userId);

            if (!result.IsCompleted)
            {
                return BadRequest();
            }
            return Ok($"Finish Purchase");
        }
    }
}
