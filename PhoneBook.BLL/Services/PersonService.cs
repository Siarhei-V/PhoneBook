using AutoMapper;
using PhoneBook.BLL.DTO;
using PhoneBook.BLL.Interfaces;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;

namespace PhoneBook.BLL.Services
{
    public class PersonService : IPersonService
    {
        IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PersonDTO> FindPerson(ISearchable searcher, string str)
        {
            return searcher.FindPersons(_unitOfWork, str);
        }

        public string SavePersons(List<PersonDTO> personDto)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<PersonDTO, CommonPersonModel>()).CreateMapper();
            var persons = mapper.Map<List<PersonDTO>, List<CommonPersonModel>>(personDto);

            return _unitOfWork.SavePersons(persons);
        }
    }
}
