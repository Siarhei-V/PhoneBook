using System;
using System.Collections.Generic;

namespace PhoneBook.OperatorUI.Models
{
    public class PersonModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string BirthDay { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class PersonsList
    {
        public List<PersonModel> PersonList { get; set; } = new List<PersonModel>() { new PersonModel() };
    }
}
