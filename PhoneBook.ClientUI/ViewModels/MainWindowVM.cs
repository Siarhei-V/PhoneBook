using AutoMapper;
using PhoneBook.BLL.DTO;
using PhoneBook.BLL.Interfaces;
using PhoneBook.BLL.Searchers;
using PhoneBook.ClientUI.Commands;
using PhoneBook.ClientUI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PhoneBook.ClientUI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        IPersonService _personService;
        PersonViewModel _personViewModel;
        SearchCriteria _searchCriteria;
        SearchCriteriaItem[] _searchCriteriaItems =             
        {
            new SearchCriteriaItem() { Searcher = new SearcherByPhone() },
            new SearchCriteriaItem() { Searcher = new SearcherByAddress() },
            new SearchCriteriaItem() { Searcher = new SearcherBySurname() },
            new SearchCriteriaItem() { Searcher = new SearcherByName() },
            new SearchCriteriaItem() { Searcher = new SearcherByPatronymic() }
        };

        public MainWindowVM(IPersonService personService, PersonViewModel personModel, SearchCriteria searchCriteria)
        {
            _personViewModel = personModel;
            _personService = personService;
            _searchCriteria = searchCriteria;
        }

        public List<PersonModel> PersonModelVM
        {
            get => _personViewModel.PersonList;
        }

        public List<string> SearchCriteriaVM
        {
            get => _searchCriteria.SearchCriteriaList;
        }

        public int SelectedSearchCriteriaVM
        {
            set => _searchCriteria.SelectedSearchCriteria = value;
        }

        public string SearchStringVM
        {
            set => _searchCriteria.SearchString = value;
        }

        public bool IsDataReceivingCompletedVM
        {
            get => _searchCriteria.IsDataReceivingCompleted;
        }


        private RelayCommand _findPersonsCommand;
        public RelayCommand FindPersonsCommand
        {
            get
            {
                return _findPersonsCommand ??
                    (_findPersonsCommand = new RelayCommand(async obj =>
                    {
                        _searchCriteria.IsDataReceivingCompleted = false;
                        OnPropertyChanged(nameof(IsDataReceivingCompletedVM));

                        var dataReceivingTask = Task.Run(() =>
                            _personService.FindPerson(_searchCriteriaItems[_searchCriteria.SelectedSearchCriteria].Searcher
                            , _searchCriteria.SearchString));

                        var personDtoList = await dataReceivingTask;

                        _searchCriteria.IsDataReceivingCompleted = true;
                        OnPropertyChanged(nameof(IsDataReceivingCompletedVM));

                        var mapper = new MapperConfiguration(m => m.CreateMap<PersonDTO, PersonModel>()).CreateMapper();
                        _personViewModel.PersonList = mapper.Map<List<PersonDTO>, List<PersonModel>>(personDtoList);

                        OnPropertyChanged(nameof(PersonModelVM));
                    }));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
