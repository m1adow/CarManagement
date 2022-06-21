using PeopleManagement.Infrastructure;
using PeopleManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;

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

        private Person _selectedPerson = new Person();

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set 
            { 
                Set(ref _selectedPerson, value);
                (DeleteCommand as RelayCommand).OnExecuteChanged();
            }
        }

        private Visibility _deleteButtonVisibility = Visibility.Collapsed;

        public Visibility DeleteButtonVisibility
        {
            get => _deleteButtonVisibility;
            set => Set(ref _deleteButtonVisibility, value);
        }

        public ObservableCollection<Person> People { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public PeopleManagementViewModel()
        {
            People = new ObservableCollection<Person>();

            AddCommand = new RelayCommand(AddPerson, IsFieldsFilled);
            DeleteCommand = new RelayCommand(DeletePerson, IsPersonSelected);
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

        #region Methods for deleting person
        private void DeletePerson() => People.Remove(SelectedPerson);

        private bool IsPersonSelected()
        {
            bool isPersonSelected = SelectedPerson != null && 
                SelectedPerson.Firstname != null && 
                SelectedPerson.Lastname != null;

            if (isPersonSelected)
                DeleteButtonVisibility = Visibility.Visible;

            return isPersonSelected;
        }
        #endregion
    }
}
