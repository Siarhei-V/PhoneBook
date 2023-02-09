using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Interfaces
{
    public interface IDataSaver
    {
        string SaveData(List<CommonPersonModel> commonPersonModels, IPersonPhoneRepository personPhoneRepository,
            IPersonAddressRepository personAddressRepository, IPersonRepository personRepository);
    }
}
