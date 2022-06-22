using PeopleManagement.ViewModel.Infrastructure;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PeopleManagement.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual void Set<T>(ref T field, T value, ICommand command = null, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
                return;

            field = value;
            OnPropertyChanged(propertyName);

            if (command != null)
                (command as RelayCommand).OnExecuteChanged();
        }
    }
}
