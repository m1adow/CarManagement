using PeopleManagement.Infrastructure;
using PeopleManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PeopleManagement.ViewModel
{
    public class PeopleManagementViewModel : INotifyPropertyChanged
    {
        private string _firstname = string.Empty;

        public string Firstname
        {
            get => _firstname;
            set
            {
                if (value != _firstname)
                {
                    _firstname = value;
                    (AddCommand as RelayCommand).OnExecuteChanged();
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        private string _lastname = string.Empty;

        public string Lastname
        {
            get => _lastname;
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    (AddCommand as RelayCommand).OnExecuteChanged();
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        public ObservableCollection<Person> People { get; private set; }

        public ICommand AddCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PeopleManagementViewModel()
        {
            People = new ObservableCollection<Person>();

            AddCommand = new RelayCommand(AddPerson, IsFieldsFilled);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddPerson()
        {
            People.Add(CreatePerson());
            ClearFields();
        }

        private bool IsFieldsFilled() => Firstname.Length > 0 && Lastname.Length > 0;

        private void ClearFields()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
        }

        private Person CreatePerson() => new Person
        {
            Firstname = Firstname,
            Lastname = Lastname
        };
    }
}
