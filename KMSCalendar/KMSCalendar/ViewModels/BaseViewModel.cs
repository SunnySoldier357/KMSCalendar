using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using KMSCalendar.Models.Entities;
using KMSCalendar.Services;

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

        public IDataStore<Assignment> DataStore =>
            DependencyService.Get<IDataStore<Assignment>>() ?? new MockDataStore<Assignment>();

        public string Title
        {
            get => title;
            set => setProperty(ref title, value);
        }

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

        //* Event & Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}