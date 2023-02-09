using PhoneBook.BLL.DTO;
using PhoneBook.DAL.Interfaces;

namespace PhoneBook.BLL.Interfaces
{
    public interface ISearchable
    {
        List<PersonDTO> FindPersons(IUnitOfWork unitOfWork, string str);
    }
}
