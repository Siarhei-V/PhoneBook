using Microsoft.EntityFrameworkCore;
using PhoneBook.DAL.EF;
using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;
using System.Text;

namespace PhoneBook.DAL.Repositories
{
    public class EFPersonPhoneRepository : IPersonPhoneRepository
    {
        readonly ApplicationContext _db;

        public EFPersonPhoneRepository(ApplicationContext context)
        {
            _db = context;
        }

        public List<CommonPersonModel> FindByPhone(string phone)
        {
            IQueryable<PersonPhone> phonesIQuer = _db.PersonPhones.Include(p => p.Person).ThenInclude(p => p.PersonAddress)
                .Include(p => p.Person).ThenInclude(p => p.PersonPhones);
            var phoneFromDb = phonesIQuer.FirstOrDefault(p => p.Phone == phone);

            if (phoneFromDb == null)
            {
                throw new ArgumentException($"Пользователь с номером {phone} отсутствует в базе");
            }

            var sb = new StringBuilder();
            
            foreach (var item in phoneFromDb.Person.PersonPhones)
            {
                sb.Append(item.Phone);
                sb.Append("\n");
            }

            sb.Remove(sb.Length - 1, 1);

            var commonModel = new CommonPersonModel()
            {
                Name = phoneFromDb.Person.Name,
                Surname = phoneFromDb.Person.Surname,
                Patronymic = phoneFromDb.Person.Patronymic,
                BirthDay = phoneFromDb.Person.BirthDay,
                Phone = sb.ToString(),
                Address = phoneFromDb.Person.PersonAddress.Address
            };

            return new List<CommonPersonModel>() { commonModel };
        }

        public PersonPhone GetPhone(Expression<Func<PersonPhone, bool>> expression)
        {
            return _db.PersonPhones.FirstOrDefault(expression);
        }

        public void SavePhones(List<PersonPhone> personPhones)
        {
            _db.AddRange(personPhones);
            _db.SaveChanges();
        }
    }
}
