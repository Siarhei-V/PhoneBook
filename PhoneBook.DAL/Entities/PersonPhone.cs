using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DAL.Entities
{
    public class PersonPhone
    {
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
