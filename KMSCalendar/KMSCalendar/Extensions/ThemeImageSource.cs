using System.ComponentModel;
using System.Runtime.CompilerServices;

using Autofac;

using KMSCalendar.Models.Settings;

using Xamarin.Forms;

namespace KMSCalendar.Extensions
{
    public class ThemeImageSource : INotifyPropertyChanged
    {
        //* Private Properties
        private readonly UserSettings userSettings;

        //* Public Properties
        public ImageSource Source { get; private set; }

        public string DarkThemeFileName { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }

        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructors
        public ThemeImageSource(string fileName, string darkThemeFileName = null,
            string folderName = null)
        {
            userSettings = AppContainer.Container.Resolve<UserSettings>();

            DarkThemeFileName = darkThemeFileName;
            FileName = fileName;
            FolderName = folderName;

            updateSource();

            if (DarkThemeFileName != null)
            {
                userSettings.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(UserSettings.Theme))
                        updateSource();
                };
            }
        }

        //* Event Handlers
        public void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //* Private Properties
        private void updateSource()
        {
            string imageName = null;
            if (DarkThemeFileName == null)
                imageName = FileName;
            else
                imageName = userSettings.Theme == Theme.Light ? FileName : DarkThemeFileName;

            Source = ImageResourceExtension.GetImageSource(imageName, FolderName);
            OnNotifyPropertyChanged(nameof(Source));
        }
    }
}