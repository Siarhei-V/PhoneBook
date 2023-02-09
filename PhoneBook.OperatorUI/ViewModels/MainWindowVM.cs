using AutoMapper;
using PhoneBook.BLL.DTO;
using PhoneBook.BLL.Interfaces;
using PhoneBook.OperatorUI.Commands;
using PhoneBook.OperatorUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.OperatorUI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        PersonsList _personsList;
        IPersonService _personService;
        string _status = "Готов. Для добавления строки нажмите Enter после ввода данных. " +
            "В графу \"Дата рождения\" вводите в формате дд.мм.гггг";

        public MainWindowVM(IPersonService personService, PersonsList personViewModel)
        {
            _personsList = personViewModel;
            _personService = personService;
        }


        public List<PersonModel> PersonModelVM
        {
            get => _personsList.PersonList;
            set => _personsList.PersonList = value;
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            } 
        }

        private RelayCommand _savePersonsCommand;
        public RelayCommand SavePersonsCommand
        {
            get
            {
                return _savePersonsCommand ??
                    (_savePersonsCommand = new RelayCommand(obj =>
                    {
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PersonModel, PersonDTO>()
                            .ForMember(dest => dest.BirthDay, o => o.MapFrom(src => DateOnly.Parse(src.BirthDay))))
                            .CreateMapper();

                        List<PersonDTO> persons = new List<PersonDTO>();

                        try
                        {
                            persons = mapper.Map<List<PersonModel>, List<PersonDTO>>(_personsList.PersonList);
                        }
                        catch (Exception)
                        {
                            Status = "Неверный формат данных";
                            return;
                        }

                        Status = _personService.SavePersons(persons);
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
