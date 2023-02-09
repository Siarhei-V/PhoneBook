using Microsoft.EntityFrameworkCore;
using PhoneBook.DAL.EF;
using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;
using System.Text;

namespace PhoneBook.DAL.Repositories
{
    public class EFPersonAddressRepository : IPersonAddressRepository
    {
        readonly ApplicationContext _db;

        public EFPersonAddressRepository(ApplicationContext context)
        {
            _db = context;
        }

        public List<CommonPersonModel> FindByAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }

            var addressFromDb = _db.PersonAddresses.Include(a => a.Persons).ThenInclude(p => p.PersonPhones)
                .FirstOrDefault(p => p.Address.Contains(address));

            if (addressFromDb == null)
            {
                return null;
            }

            var commonModelsList = new List<CommonPersonModel>();
            var addressForCommonModel = addressFromDb.Address;

            for (int i = 0; i < addressFromDb.Persons.Count; i++)
            {
                var sb = new StringBuilder();

                foreach (var item in addressFromDb.Persons[i].PersonPhones)
                {
                    sb.Append(item.Phone);
                    sb.Append("\n");
                }

                sb.Remove(sb.Length - 1, 1);
                
                commonModelsList.Add(
                    new CommonPersonModel() { 
                        Name = addressFromDb.Persons[i].Name,
                        Surname = addressFromDb.Persons[i].Surname,
                        Patronymic = addressFromDb.Persons[i].Patronymic,
                        BirthDay = addressFromDb.Persons[i].BirthDay,
                        Phone = sb.ToString(),
                        Address = addressForCommonModel,
                    });
            }

            return commonModelsList;
        }

        public PersonAddress GetAddress(Expression<Func<PersonAddress, bool>> expression)
        {
            return _db.PersonAddresses.FirstOrDefault(expression);
        }

        public void SaveAddresses(List<PersonAddress> personAddresses)
        {
            _db.AddRange(personAddresses);
            _db.SaveChanges();
        }
    }
}
