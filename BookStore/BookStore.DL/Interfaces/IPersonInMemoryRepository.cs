using BookStore.Models;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DL.Interfaces
{
    public interface IPersonInMemoryRepository
    {
        IEnumerable<Person> GetAllUsers();
        Person GetById(int id);
        Person? AddUser(Person user);
        Person? UpdateUser(Person user);
        Person? DeleteUserById(int id);
        Guid GetGuidId();
    }
}
