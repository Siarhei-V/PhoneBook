using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IPersonRepository _personsRepository;
        readonly IPersonPhoneRepository _personPhoneRepository;
        readonly IPersonAddressRepository _personAddressRepository;
        readonly IDataSaver _dataSaver;

        public UnitOfWork(IPersonRepository personsRepository, IPersonPhoneRepository personPhoneRepository
            , IPersonAddressRepository personAddressRepository, IDataSaver dataSaver)
        {
            _personsRepository = personsRepository;
            _personPhoneRepository = personPhoneRepository;
            _personAddressRepository = personAddressRepository;
            _dataSaver = dataSaver;
        }

        public IPersonRepository Persons => _personsRepository;

        public IPersonPhoneRepository PersonPhones => _personPhoneRepository;

        public IPersonAddressRepository PersonAddresses => _personAddressRepository;

        public string SavePersons(List<CommonPersonModel> commonPersonModels)
        {
            return _dataSaver.SaveData(commonPersonModels, _personPhoneRepository, _personAddressRepository, _personsRepository);
        }
    }
}
