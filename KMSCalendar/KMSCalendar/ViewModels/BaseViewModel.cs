using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using ModelValidation;

using KMSCalendar.Models.Settings;

namespace KMSCalendar.ViewModels
{
    public abstract class BaseViewModel : ValidatableObject, INotifyPropertyChanged
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

        public UserSettings Settings { get; }

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

        //* Constructors
        public BaseViewModel() => 
            Settings = UserSettings.DefaultInstance;

        //* Event Handlers
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            OnNotifyPropertyChanged(propertyName);
    }
}