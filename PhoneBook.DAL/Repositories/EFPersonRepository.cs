using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DAL.EF;
using PhoneBook.DAL.Entities;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;
using System.Linq.Expressions;
using System.Text;

namespace PhoneBook.DAL.Repositories
{
    public class EFPersonRepository : IPersonRepository
    {
        readonly ApplicationContext _db;

        public EFPersonRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public List<CommonPersonModel> FindPersons(Expression<Func<Person, bool>> expression)
        {
            IQueryable<Person> personsIQuer = _db.Persons.Include(a => a.PersonAddress).Include(p => p.PersonPhones);
            var persons = personsIQuer.Where(expression).ToList();

            var mapper = new MapperConfiguration(m => m.CreateMap<Person, CommonPersonModel>()).CreateMapper();
            var commonModel = mapper.Map<List<Person>, List<CommonPersonModel>>(persons);
            

            for (int i = 0; i < persons.Count; i++)
            {
                commonModel[i].Address = persons[i].PersonAddress.Address;

                var sb = new StringBuilder();
                
                foreach (var item in persons[i].PersonPhones)
                {
                    sb.Append(item.Phone);
                    sb.Append("\n");
                }
                
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                commonModel[i].Phone = sb.ToString();
            }

            return commonModel;
        }

        public Person GetPerson(Expression<Func<Person, bool>> expression)
        {
            return _db.Persons.FirstOrDefault(expression);
        }

        public void SavePersons(List<Person> person)
        {
            _db.AddRange(person);
            _db.SaveChanges();
        }
    }
}
