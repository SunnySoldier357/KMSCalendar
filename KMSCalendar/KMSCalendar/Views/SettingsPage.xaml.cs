using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using System.Collections.Generic;

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

            var pickerItems = new List<ThemeItem>
            {
                new ThemeItem
                {
                    Name = nameof(Theme.Light),
                    Theme = Theme.Light
                },
                new ThemeItem
                {
                    Name = nameof(Theme.Dark),
                    Theme = Theme.Dark
                }
            };

            ThemePicker.ItemsSource = pickerItems;
            ThemePicker.SelectedItem = pickerItems.First(a => a.Theme == settings.Theme);
        }

        //* Event Handlers
        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ThemePicker.SelectedItem as ThemeItem;

            if (item != null)
                settings.Theme = item.Theme;
        }
    }
}