using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPeople();
        Person GetById(int id);
        AddPersonResponse AddPerson(AddPersonRequest personRequest);
        UpdatePersonResponse UpdatePerson(UpdatePersonRequest authorRequest);
        Person DeletePersonById(int id);
    }
}
