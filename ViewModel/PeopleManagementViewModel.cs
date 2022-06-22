using PeopleManagement.Models;
using PeopleManagement.ViewModel.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace PeopleManagement.ViewModel
{
    public class PeopleManagementViewModel : ViewModelBase
    {
        private string _firstname = string.Empty;

        public string Firstname
        {
            get => _firstname;
            set => Set(ref _firstname, value, AddCommand);
        }

        private string _lastname = string.Empty;

        public string Lastname
        {
            get => _lastname;
            set => Set(ref _lastname, value, AddCommand);
        }

        private Person _selectedPerson = new Person();

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value, DeleteCommand);
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

        private bool _isDeletedConfrimed = false;

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
        private async void DeletePerson()
        {
            await ShowConfirmingDialog();

            if (!_isDeletedConfrimed)
                return;

            People.Remove(SelectedPerson);
            DeleteButtonVisibility = Visibility.Collapsed;
        }

        private async Task ShowConfirmingDialog()
        {
            MessageDialog messageDialog = new MessageDialog("Are you sure for deleting?");

            messageDialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command) => _isDeletedConfrimed = command.Label == "OK";


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
