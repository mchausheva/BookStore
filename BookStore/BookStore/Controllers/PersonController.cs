using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllPeople();
        }

        [HttpGet(nameof(GetById))]
        public Person? GetById(int id)
        {
            return _personService.GetById(id);
        }
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Person person)
        {
            _personService.AddPerson(person);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Person person)
        {
            _personService.UpdatePerson(person);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Person? DeleteMethod(int id)
        {
            return _personService.DeletePersonById(id);
        }
    }
}