using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMSCalendar.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //* Private Properties
        private bool isBusy = false;

        private string title = string.Empty;

        //* Public Properties
        public bool IsBusy
        {
            get => isBusy;
            set => setProperty(ref isBusy, value);
        }

        public string Title
        {
            get => title;
            set => setProperty(ref title, value);
        }

        //* Public Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Protected Methods
        protected bool setProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        //* Event Handlers
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}