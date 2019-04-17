using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KMSCalendar.Models
{
    public class Settings : INotifyPropertyChanged
    {
        //* Constants
        private const string DIC_KEY = "Settings";

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
        public Settings()
        {
            // Default Values
            theme = Theme.Light;
        }

        public Settings(Settings settings) => 
            Theme = settings.Theme;

        //* Static Methods
        public static async Task InitAsync(Settings settings)
        {
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