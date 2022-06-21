using PeopleManagement.Infrastructure;
using PeopleManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PeopleManagement.ViewModel
{
    public class PeopleManagementViewModel : ViewModelBase
    {
        private string _firstname = string.Empty;

        public string Firstname
        {
            get => _firstname;
            set
            {
                Set(ref _firstname, value);
                (AddCommand as RelayCommand).OnExecuteChanged();
            }
        }

        private string _lastname = string.Empty;

        public string Lastname
        {
            get => _lastname;
            set
            {
                Set(ref _lastname, value);
                (AddCommand as RelayCommand).OnExecuteChanged();
            }
        }

        public ObservableCollection<Person> People { get; private set; }

        public ICommand AddCommand { get; private set; }

        public PeopleManagementViewModel()
        {
            People = new ObservableCollection<Person>();

            AddCommand = new RelayCommand(AddPerson, IsFieldsFilled);
        }

        #region Methods for adding person
        private void AddPerson()
        {
            People.Add(CreatePerson());
            ClearFields();
        }  

        private Person CreatePerson() => new Person
        {
            Firstname = Firstname,
            Lastname = Lastname
        };

        private void ClearFields()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
        }

        private bool IsFieldsFilled() => Firstname.Length > 0 && Lastname.Length > 0;
        #endregion
    }
}
