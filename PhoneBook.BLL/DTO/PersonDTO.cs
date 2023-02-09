namespace PhoneBook.BLL.DTO
{
    public class PersonDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
