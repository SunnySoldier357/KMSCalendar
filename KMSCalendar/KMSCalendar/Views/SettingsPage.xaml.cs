using System;
using System.Globalization;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        //* Private Properties
        public Settings settings = Settings.DefaultInstance;

        public SettingsPage()
        {
            InitializeComponent();

            var pickerItems = new[]
            {
                new
                {
                    Name = nameof(Theme.Light),
                    Value = Theme.Light
                },
                new
                {
                    Name = nameof(Theme.Dark),
                    Value = Theme.Dark
                }
            }.ToList();

            ThemePicker.ItemsSource = pickerItems;
            ThemePicker.ItemDisplayBinding = new Binding("Name");
            ThemePicker.SelectedItem = pickerItems.First(a => a.Value == settings.Theme);
        }

        //* Event Handlers
        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ThemePicker.SelectedItem;

            var a = new { Name = "", Value = Theme.Dark };

            a = Cast(a, item);

            if (a != null)
                settings.Theme = a.Value;
        }

        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
        }
    }

    public class ThemeItem
    {
        //* Public Properties
        public Theme Value;

        public string Name;

        public override string ToString() => Name;
    }

    // Converts from Theme to Color
    public class ThemeToColorConverter : IValueConverter
    {
        //* Interface Implementations
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value as Theme?)
            {
                case Theme.Dark:
                    return Color.Black;
                case Theme.Light:
                default:
                    return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? valueColor = value as Color?;

            if (valueColor == Color.Black)
                return Theme.Dark;
            
            return Theme.Light;
        }
    }
}