using System;
using System.Collections.Generic;

namespace PhoneBook.ClientUI.Models
{
    public class PersonModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PersonViewModel
    {
        public List<PersonModel> PersonList { get; set; } = new List<PersonModel>();
    }
}
