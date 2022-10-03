using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPersonInMemoryRepository
    {
        IEnumerable<Person> GetAllPeople();
        Person GetById(int id);
        Person? AddPerson(Person person);
        Person? UpdatePerson(Person person);
        Person? DeletePersonById(int id);
    }
}
