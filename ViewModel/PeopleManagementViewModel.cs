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
            set => Set(ref _firstname, value, new ICommand[] { AddCommand });
        }

        private string _lastname = string.Empty;

        public string Lastname
        {
            get => _lastname;
            set => Set(ref _lastname, value, new ICommand[] { AddCommand });
        }

        private Person _selectedPerson = new Person();

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value, new ICommand[] { DeleteCommand, UpdateCommand });
        }

        private Visibility _addButtonVisibility = Visibility.Visible;

        public Visibility AddButtonVisibility
        {
            get => _addButtonVisibility;
            set => Set(ref _addButtonVisibility, value);
        }

        private Visibility _deleteButtonVisibility = Visibility.Collapsed;

        public Visibility DeleteButtonVisibility
        {
            get => _deleteButtonVisibility;
            set => Set(ref _deleteButtonVisibility, value);
        }

        private Visibility _updateButtonVisibility = Visibility.Collapsed;

        public Visibility UpdateButtonVisibility
        {
            get => _updateButtonVisibility;
            set => Set(ref _updateButtonVisibility, value);
        }

        public ObservableCollection<Person> People { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand UpdateCommand { get; private set; }

        private bool _isDeletedConfrimed = false;

        public PeopleManagementViewModel()
        {
            People = new ObservableCollection<Person>();

            AddCommand = new RelayCommand(AddPerson, IsFieldsFilled);
            DeleteCommand = new RelayCommand(DeletePerson, IsPersonSelected);
            UpdateCommand = new RelayCommand(UpdatePerson);
        }

        #region Methods for adding person
        private void AddPerson()
        {
            People.Add(CreatePerson());
            ClearFields();
        }

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

            ResetFields();

            if (!_isDeletedConfrimed)
                return;

            People.Remove(SelectedPerson);
            ResetSelectedPerson();
        }

        private async Task ShowConfirmingDialog()
        {
            MessageDialog messageDialog = new MessageDialog("Are you sure for deleting?");

            messageDialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            _isDeletedConfrimed = command.Label == "OK";
            ChangeVisibility(Visibility.Collapsed);
        }

        private void ResetFields()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
        }

        private bool IsPersonSelected()
        {
            bool isPersonSelected = SelectedPerson != null &&
                SelectedPerson.Firstname != null &&
                SelectedPerson.Lastname != null;

            if (isPersonSelected)
            {
                ChangeVisibility(Visibility.Visible);
                ShowSelectedPersonFields();
            }
                
            return isPersonSelected;
        }

        private void ShowSelectedPersonFields()
        {
            Firstname = SelectedPerson.Firstname;
            Lastname = SelectedPerson.Lastname;
        }
        #endregion

        #region Methods for updating person
        private void UpdatePerson()
        {
            UpdatePersonInCollection();
            ChangeVisibility(Visibility.Collapsed);
            ResetSelectedPerson();
        }

        private void UpdatePersonInCollection()
        {
            int index = People.IndexOf(SelectedPerson);
            People.RemoveAt(index);
            People.Insert(index, CreatePerson());
        }
        #endregion

        #region Methods which using in all commands
        private Person CreatePerson() => new Person
        {
            Firstname = Firstname,
            Lastname = Lastname
        };

        private void ChangeVisibility(Visibility visibility)
        {
            DeleteButtonVisibility = visibility;
            UpdateButtonVisibility = visibility;

            if (visibility == 0)
                AddButtonVisibility = Visibility.Collapsed;
            else
                AddButtonVisibility = Visibility.Visible;
        }

        private void ResetSelectedPerson() => _selectedPerson = new Person();
        #endregion
    }
}
