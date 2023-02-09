using AutoMapper;
using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL
{
    public class PersonsSaver : IDataSaver
    {
        public string SaveData(List<CommonPersonModel> commonPersonModels, IPersonPhoneRepository personPhoneRepository,
            IPersonAddressRepository personAddressRepository, IPersonRepository personRepository)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<CommonPersonModel, Person>()).CreateMapper();
            var persons = mapper.Map<List<CommonPersonModel>, List<Person>>(commonPersonModels);

            mapper = new MapperConfiguration(m => m.CreateMap<CommonPersonModel, PersonPhone>()).CreateMapper();
            var personPhones = mapper.Map<List<CommonPersonModel>, List<PersonPhone>>(commonPersonModels);

            mapper = new MapperConfiguration(m => m.CreateMap<CommonPersonModel, PersonAddress>()).CreateMapper();
            var personAddresses = mapper.Map<List<CommonPersonModel>, List<PersonAddress>>(commonPersonModels);

            var filteredPersonAddresses = personAddresses.GroupBy(x => x.Address).Select(x => x.First()).ToList();
            var resultAddresses = new List<PersonAddress>();

            // TODO: I think I should have used GUID

            foreach (var item in personPhones)
            {
                var phone = personPhoneRepository.GetPhone(p => p.Phone == item.Phone);

                if (phone != null)
                {
                    return $"Пользователь с телефоном {phone} уже существует";
                }
            }

            foreach (var item in filteredPersonAddresses)
            {
                var address = personAddressRepository.GetAddress(p => p.Address == item.Address);

                if (address == null)
                {
                    resultAddresses.Add(item);
                }
            }
            
            personAddressRepository.SaveAddresses(resultAddresses);

            for (int i = 0; i < commonPersonModels.Count; i++)
            {
                var address = personAddressRepository.GetAddress(p => p.Address == commonPersonModels[i].Address);
                persons[i].PersonAddressId = address.Id;
            }

            personRepository.SavePersons(persons);

            for (int i = 0; i < commonPersonModels.Count; i++)
            {
                var person = personRepository.GetPerson(p => p.Name == commonPersonModels[i].Name
                                                            && p.Surname == commonPersonModels[i].Surname
                                                            && p.Patronymic == commonPersonModels[i].Patronymic
                                                            && p.BirthDay == commonPersonModels[i].BirthDay);

                personPhones[i].PersonId = person.Id;
            }

            personPhoneRepository.SavePhones(personPhones);

            return "Данные успешно сохранены";
        }
    }
}
