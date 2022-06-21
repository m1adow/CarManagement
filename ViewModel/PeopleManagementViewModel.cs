using CarManagement.Models;
using CarManagement.ViewModel.Commands;
using System.ComponentModel;

namespace CarManagement.ViewModel
{
    public class PeopleManagementViewModel : INotifyPropertyChanged
    {
        private string _firstname;

        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                AddCommand.OnCanExecuteChanged();
                OnPropertyChanged(nameof(Firstname));
            }
        }

        private string _lastname;

        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                AddCommand.OnCanExecuteChanged();
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public AddCommand AddCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PeopleManagementViewModel()
        {
            AddCommand = new AddCommand(this);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public bool IsFieldsFilled()
        {
            if (Firstname is null || Lastname is null)
                return false;

            if (Firstname.Length > 0 && Lastname.Length > 0)
                return true;

            return false;
        }

        public void ClearFields()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
        }

        public Person CreatePerson() => new Person
        {
            Firstname = Firstname,
            Lastname = Lastname
        };      
    }
}
