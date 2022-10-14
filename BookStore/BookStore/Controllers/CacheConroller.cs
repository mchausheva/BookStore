using BookStore.Caches.Cache;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheConroller : ControllerBase
    {
        private readonly CacheService<int, Book> _cacheService;
        private readonly ILogger<CacheConroller> _logger;
        public CacheConroller(CacheService<int, Book> cacheService , ILogger<CacheConroller> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBooks))]
        public IActionResult GetBooks()
        {
            var result = _cacheService.GetCache(new CancellationToken());
            return Ok();
        }
    }
}
