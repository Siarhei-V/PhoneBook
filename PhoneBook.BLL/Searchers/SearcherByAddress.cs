using AutoMapper;
using PhoneBook.BLL.DTO;
using PhoneBook.BLL.Interfaces;
using PhoneBook.DAL.Interfaces;
using PhoneBook.DAL.Models;

namespace PhoneBook.BLL.Searchers
{
    public class SearcherByAddress : ISearchable
    {
        public List<PersonDTO> FindPersons(IUnitOfWork unitOfWork, string str)
        {
            try
            {
                var persons = unitOfWork.PersonAddresses.FindByAddress(str);

                var mapper = new MapperConfiguration(m => m.CreateMap<CommonPersonModel, PersonDTO>()).CreateMapper();
                return mapper.Map<List<CommonPersonModel>, List<PersonDTO>>(persons);
            }
            catch (Exception ex)
            {
                return null; // TODO: implement exceptions handling
            }
        }
    }
}
