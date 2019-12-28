using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KMSCalendar.Views;
using ModelValidation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
    public abstract class BaseViewModel : ValidatableObject, INotifyPropertyChanged
    {
        //* Public Properties
        public App App => Application.Current as App;

        //* Private Properties
        private bool isBusy = false;
        private string title = string.Empty;

        //* Constructor
        public BaseViewModel() =>
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            OnNotifyPropertyChanged(propertyName);

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
                App.MainPage.Navigation.PushModalAsync(new NetworkFailPage());
        }

    }
}