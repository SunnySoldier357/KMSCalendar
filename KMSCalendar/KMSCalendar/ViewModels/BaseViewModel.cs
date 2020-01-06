using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using KMSCalendar.Services.Data;
using KMSCalendar.Views;

using ModelValidation;

using PropertyChanged;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public abstract class BaseViewModel : ValidatableObject, INotifyPropertyChanged
	{
		//* Public Properties
		public App App => Application.Current as App;
		public DataOperation DataOperation = new DataOperation();

		//* Constructor
		public BaseViewModel() =>
			Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

		//* Public Properties
		public bool IsBusy { get; set; } = false;

		public string Title { get; set; }

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

		private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
			if (e.NetworkAccess != NetworkAccess.Internet)
				App.MainPage.Navigation.PushModalAsync(new NetworkFailPage());
		}
	}
}