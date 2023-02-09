using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }
        IPersonPhoneRepository PersonPhones { get; }
        IPersonAddressRepository PersonAddresses { get; }
        string SavePersons(List<CommonPersonModel> commonPersonModels);
    }
}
