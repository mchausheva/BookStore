using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPeople();
        Person GetById(int id);
        Person GetByName(string name);
        Person AddPerson(Person person);
        Person UpdatePerson(Person person);
        Person DeletePersonById(int id);
    }
}
