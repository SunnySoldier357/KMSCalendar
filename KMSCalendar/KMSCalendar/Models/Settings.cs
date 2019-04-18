using Newtonsoft.Json;
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
        private Theme theme;

        //* Public Properties
        public Theme Theme
        {
            get => theme;
            set
            {
                if (theme != value)
                {
                    theme = value;
                    OnNotifyPropertyChanged(nameof(Theme));
                    UpdateDictionaryAsync();
                }
            }
        }

        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructor
        private Settings()
        {
            // Default Values
            theme = Theme.Light;
        }

        private Settings(Settings settings)
        {
            if (settings != null)
                Theme = settings.Theme;
        }
            
        //* Static Methods
        private static async Task<Settings> initAsync()
        {
            Settings settings = null;

            string settingsJson = null;

            if (App.Current.Properties.ContainsKey(DIC_KEY))
                settingsJson = App.Current.Properties[DIC_KEY] as string;

            if (settingsJson != null)
            {
                Settings temp = JsonConvert.DeserializeObject<Settings>(settingsJson);

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
    }

    public enum Theme
    {
        Light,
        Dark
    }
}