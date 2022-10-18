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
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cacheService.GetCacheDict();
            return Ok(result);
        }
    }
}
