using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CarManagement.ViewModel.Commands
{
    public class AddCommand : ICommand
    {
        public PeopleManagementViewModel PeopleManagementViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public AddCommand(PeopleManagementViewModel peopleManagementViewModel)
        {
            PeopleManagementViewModel = peopleManagementViewModel;
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => PeopleManagementViewModel.IsFieldsFilled();

        public void Execute(object parameter)
        {
            ListView listView = parameter as ListView;

            if (listView is null)
                return;

            listView.Items.Add(PeopleManagementViewModel.CreatePerson());
        }
    }
}
