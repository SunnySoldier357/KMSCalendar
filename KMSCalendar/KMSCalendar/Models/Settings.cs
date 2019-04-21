﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KMSCalendar.Models
{
    public class Settings : INotifyPropertyChanged
    {
        //* Constants
        private const string DIC_KEY = "Settings";

        //* Static Properties
        public static Settings DefaultInstance = initAsync().Result;

        //* Private Properties
        private bool showCalendarDays;

        private Theme theme;

        //* Public Properties
        public bool ShowCalendarDays
        {
            get => showCalendarDays;
            set => modifyProperty(ref value, ref showCalendarDays, nameof(ShowCalendarDays));
        }

        public Theme Theme
        {
            get => theme;
            set => modifyProperty(ref value, ref theme, nameof(Theme));
        }

        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructor
        private Settings()
        {
            // Default Values
            showCalendarDays = false;

            theme = Theme.Light;
        }

        private Settings(Settings settings)
        {
            if (settings != null)
            {
                ShowCalendarDays = settings.ShowCalendarDays;

                Theme = settings.Theme;
            }  
        }
            
        //* Static Methods
        private static async Task<Settings> initAsync()
        {
            Settings settings = null;

            object settingsJson = null;

            App.Current.Properties.TryGetValue(DIC_KEY, out settingsJson);

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
            if (!value.Equals(privateProperty))
            {
                privateProperty = value;
                OnNotifyPropertyChanged(nameOfProperty);
                UpdateDictionaryAsync();
            }
        }
    }

    public enum Theme
    {
        Light,
        Dark
    }
}