using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DAL.Entities
{
    public class PersonAddress
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}
