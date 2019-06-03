﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KMSCalendar.Models
{
    /// <summary>
    /// A class that allows app settings to be saved between sessions.
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        //* Constants

        /// <summary>
        /// The key for App Settings in the App's Properties Dictionary
        /// </summary>
        private const string DIC_KEY = "Settings";

        //* Static Properties

        /// <summary>Singleton Instance for Settings class</summary>
        public static Settings DefaultInstance = initAsync().Result;

        //* Private Properties
        private bool showCalendarDays;

        private string signedInUserId;

        private Theme theme;

        //* Public Properties

        /// <summary>
        /// If <see langword="true"/>, the WeekControl will display the days of the
        /// week.
        /// </summary>
        public bool ShowCalendarDays
        {
            get => showCalendarDays;
            set => modifyProperty(ref value, ref showCalendarDays, nameof(ShowCalendarDays));
        }

        /// <summary>
        /// The ID of the User signed in.
        /// </summary>
        public string SignedInUserId
        {
            get => signedInUserId;
            set => modifyProperty(ref value, ref signedInUserId, nameof(SignedInUserId));
        }

        /// <summary>
        /// The Theme (Light or Dark) that the Application should be themed in.
        /// </summary>
        public Theme Theme
        {
            get => theme;
            set => modifyProperty(ref value, ref theme, nameof(Theme));
        }

        //* Events

        /// <summary>
        /// Event is invoked whenever a public property of Settings is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructor
        private Settings()
        {
            // Default Values
            showCalendarDays = false;

            signedInUserId = null;

            theme = Theme.Light;
        }

        private Settings(Settings settings)
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
        private static async Task<Settings> initAsync()
        {
            Settings settings = null;

            App.Current.Properties.TryGetValue(DIC_KEY, out object settingsJson);

            if (settingsJson != null)
            {
                Settings temp = JsonConvert.DeserializeObject<Settings>(settingsJson as string);

                settings = new Settings(temp);
            }
            else
            {
                settings = new Settings();

                await settings.UpdateDictionaryAsync();
            }

            return settings;
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
        private void modifyProperty<T>(ref T value, ref T privateProperty, string nameOfProperty)
        {
            if (value == null && privateProperty == null)
                return;
            else if ((value == null && privateProperty != null) || !value.Equals(privateProperty))
            {
                privateProperty = value;
                OnNotifyPropertyChanged(nameOfProperty);
                UpdateDictionaryAsync();
            }
        }
    }
}