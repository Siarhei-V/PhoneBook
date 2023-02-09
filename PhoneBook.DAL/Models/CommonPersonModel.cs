namespace PhoneBook.DAL.Models
{
    public class CommonPersonModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
