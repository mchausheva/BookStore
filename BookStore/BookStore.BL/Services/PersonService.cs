using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonInMemoryRepository _personRepository;
        public PersonService(IPersonInMemoryRepository personService)
        {
            _personRepository = personService;
        }
        public IEnumerable<Person?> GetAllUsers()
        {
            return _personRepository.GetAllUsers();
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }
        public Person? AddUser(Person person)
        {
            return _personRepository.AddUser(person);
        }
        public Person? UpdateUser(Person person)
        {
            return _personRepository.UpdateUser(person);
        }
        public Person? DeleteUserById(int id)
        {
            return _personRepository.DeleteUserById(id);
        }
    }
}