using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;

namespace PhoneBook.DAL.Interfaces
{
    public interface IPersonRepository
    {
        List<CommonPersonModel> FindPersons(Expression<Func<Person, bool>> predicate);
        Person GetPerson(Expression<Func<Person, bool>> expression);
        void SavePersons(List<Person> person);
    }
}
