﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace KMSCalendar.Models.Settings
{
	/// <summary>
	/// A class that allows app settings to be saved between sessions.
	/// </summary>
	public class UserSettings : INotifyPropertyChanged
	{
		//* Constants

		/// <summary>
		/// The key for User Settings in the App's Properties Dictionary
		/// </summary>
		private const string DIC_KEY = "Settings";

		//* Static Properties
		private static bool isInitialized = false;

		//* Private Properties
		private bool showCalendarDays;

		private Guid signedInUserId;

		private Theme theme;

		//* Public Properties

		/// <summary>
		/// If <see langword="true"/>, the WeekControl will display the days of the
		/// week.
		/// </summary>
		public bool ShowCalendarDays
		{
			get => showCalendarDays;
			set => setProperty(ref showCalendarDays, value);
		}

		/// <summary>
		/// The ID of the User signed in.
		/// </summary>
		public Guid SignedInUserId
		{
			get => signedInUserId;
			set => setProperty(ref signedInUserId, value);
		}

		/// <summary>
		/// The Theme (Light or Dark) that the Application should be themed in.
		/// </summary>
		public Theme Theme
		{
			get => theme;
			set => setProperty(ref theme, value);
		}

		//* Events

		/// <summary>
		/// Event is invoked whenever a public property of Settings is changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		//* Constructor
		private UserSettings()
		{
			// Default Values
			showCalendarDays = false;

			signedInUserId = Guid.Empty;

			theme = Theme.Light;
		}

		private UserSettings(UserSettings settings)
		{
			if (settings != null)
			{
				ShowCalendarDays = settings.ShowCalendarDays;

				SignedInUserId = settings.SignedInUserId;

				Theme = settings.Theme;
			}
		}

		//* Static Methods

		/// <summary>
		/// Initialises the Singleton Settings class either with values pulled out
		/// from the App's Properties Dictionary or setting the default values
		/// </summary>
		/// <returns>The Settings object created.</returns>
		public static UserSettings InitSingleton()
		{
			if (!isInitialized)
			{
				UserSettings settings = null;

				App.Current.Properties.TryGetValue(DIC_KEY, out object settingsJson);

				if (settingsJson != null)
				{
					UserSettings temp = JsonConvert.DeserializeObject<UserSettings>(settingsJson as string);

					settings = new UserSettings(temp);
				}
				else
				{
					settings = new UserSettings();

					_ = settings.UpdateDictionaryAsync();
				}

				return settings;
			}

			throw new Exception($"A Singleton Instance of {nameof(UserSettings)} has already " +
				"been initialized.");
		}

		//* Public Methods

		/// <summary>
		/// Updates the App's Property Dictionary based on the current values in the instance.
		/// </summary>
		public async Task UpdateDictionaryAsync()
		{
			string settingsJson = JsonConvert.SerializeObject(this);
			App.Current.Properties[DIC_KEY] = settingsJson;

			await App.Current.SavePropertiesAsync();
		}

		//* Event Handlers
		public void OnNotifyPropertyChanged(string property) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

		//* Private Methods
		private bool setProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			_ = UpdateDictionaryAsync();
			OnNotifyPropertyChanged(propertyName);

			return true;
		}
	}
}