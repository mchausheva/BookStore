using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllUsers();
        Person GetById(int id);
        Person? AddUser(Person user);
        Person? UpdateUser(Person user);
        Person? DeleteUserById(int id);
    }
}
