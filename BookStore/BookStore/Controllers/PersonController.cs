using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonInMemoryRepository _personInMemoryRepository;
        private readonly ILogger<PersonController> _logger;
        public PersonController(ILogger<PersonController> logger, IPersonInMemoryRepository userInMemoryRepository)
        {
            _logger = logger;
            _personInMemoryRepository = userInMemoryRepository;
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _personInMemoryRepository.GetGuidId();
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return _personInMemoryRepository.GetAllUsers();
        }

        [HttpGet(nameof(GetById))]
        public Person? GetById(int id)
        {
            return _personInMemoryRepository.GetById(id);
        }
        //[HttpGet(nameof(GetByFirstName))]
        //public IEnumerable<User> GetByFirstName(string name)
        //{
        //    return _userInMemoryRepository.Where(tm => tm.FirstName == name);
        //}
        [HttpPost(nameof(AddMethod))]
        public void AddMethod(Person user)
        {
            _personInMemoryRepository.AddUser(user);
        }
        [HttpPut(nameof(UpdateMethod))]
        public void UpdateMethod(Person user)
        {
            _personInMemoryRepository.UpdateUser(user);
        }
        [HttpDelete(nameof(DeleteMethod))]
        public Person? DeleteMethod(int id)
        {
            return _personInMemoryRepository.DeleteUserById(id);
        }
    }
}