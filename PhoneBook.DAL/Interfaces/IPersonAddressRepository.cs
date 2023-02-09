using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;

namespace PhoneBook.DAL.Interfaces
{
    public interface IPersonAddressRepository
    {
        List<CommonPersonModel> FindByAddress(string address);
        PersonAddress GetAddress(Expression<Func<PersonAddress, bool>> expression);
        void SaveAddresses(List<PersonAddress> personAddresses);
    }
}
