using PhoneBook.BLL.DTO;

namespace PhoneBook.BLL.Interfaces
{
    public interface IPersonService
    {
        List<PersonDTO> FindPerson(ISearchable searcher, string str);
        string SavePersons(List<PersonDTO> personDto);
    }
}
