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
        public IEnumerable<Person?> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }
        public Person? AddPerson(Person person)
        {
            return _personRepository.AddPerson(person);
        }
        public Person? DeletePersonById(int id)
        {
            return _personRepository.DeletePersonById(id);
        }
        public Person? UpdatePerson(Person person)
        {
            return _personRepository.UpdatePerson(person);
        }
    }
}