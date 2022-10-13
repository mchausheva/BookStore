using BookStore.BL.Kafka;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PSController : ControllerBase
    {
        private readonly ProducerService<int, int> _producer;
        private readonly ILogger<PSController> _logger;
        public PSController(ProducerService<int, int> producer, ILogger<PSController> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(SendMessage))]
        public IActionResult SendMessage([FromBody]int key, int value)
        {
            _producer.SendMessage(key, value);
            return Ok();
        }
    }
}
