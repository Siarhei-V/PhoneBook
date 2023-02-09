using PhoneBook.BLL.Interfaces;
using System.Collections.Generic;

namespace PhoneBook.ClientUI.Models
{
    public class SearchCriteria
    {
        public List<string> SearchCriteriaList { get; set; } = new List<string>()
        {
            "Телефон",
            "Адрес",
            "Фамилия",
            "Имя",
            "Отчество",
        };

        public int SelectedSearchCriteria { get; set; }
        public string SearchString { get; set; }
        public bool IsDataReceivingCompleted { get; set; } = true;
    }

    public class SearchCriteriaItem
    {
        public ISearchable Searcher { get; set; }
    }
}
