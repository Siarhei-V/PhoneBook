using AutoMapper;
using PhoneBook.BLL.DTO;
using PhoneBook.BLL.Interfaces;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;

namespace PhoneBook.BLL.Searchers
{
    public class SearcherByName : ISearchable
    {
        public List<PersonDTO> FindPersons(IUnitOfWork unitOfWork, string str)
        {
            var persons = unitOfWork.Persons.FindPersons(p => p.Name == str);

            if (persons == null)
            {
                throw new ArgumentException("Пользователи не найдены");
            }

            var mapper = new MapperConfiguration(m => m.CreateMap<CommonPersonModel, PersonDTO>()).CreateMapper();
            return mapper.Map<List<CommonPersonModel>, List<PersonDTO>>(persons);
        }
    }
}
