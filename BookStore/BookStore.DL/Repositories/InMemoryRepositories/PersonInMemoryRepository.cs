using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class PersonInMemoryRepository : IPersonInMemoryRepository
    {
        private static List<Person> _persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                FirstName = "Gosho",
                LastName = "Goshov",
                Age = 22,
            },
            new Person()
            {
                Id = 2,
                FirstName = "Pesho",
                LastName = "Peshov",
                Age = 33
            }
        };

        public IEnumerable<Person> GetAllPeople()
        {
            return _persons;
        }
        public Person GetById(int id)
        {
            return _persons.FirstOrDefault(u => u.Id == id);
        }
        public Person? AddPerson (Person person)
        {
            try
            {
                _persons.Add(person);
            }
            catch (Exception ex)
            {
                return null;
            }
            return person;
        }
        public Person? UpdatePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(u => u.Id == person.Id);
            if (existingPerson == null) return null;
            _persons.Remove(existingPerson);
            _persons.Add(person);
            return person;
        }
        public Person? DeletePersonById(int id)
        {
            if (id <= 0) return null;
            var person = _persons.FirstOrDefault(u => u.Id == id);
            _persons.Remove(person);
            return person;
        }
    }
}