using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;

namespace PhoneBook.DAL.Interfaces
{
    public interface IPersonPhoneRepository
    {
        PersonPhone GetPhone(Expression<Func<PersonPhone, bool>> expression);
        List<CommonPersonModel> FindByPhone(string phone);
        void SavePhones(List<PersonPhone> personPhones);
    }
}
