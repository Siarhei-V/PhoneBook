using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DAL.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [MaxLength(50)]
        public string Patronymic { get; set; }
        
        public DateOnly BirthDay { get; set; }

        [Required]
        public int PersonAddressId { get; set; }
        public PersonAddress PersonAddress { get; set; }
        public List<PersonPhone> PersonPhones { get; set; }
    }
}
