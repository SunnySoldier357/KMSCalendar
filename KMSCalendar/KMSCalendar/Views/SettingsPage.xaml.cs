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

        //* Constructor
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

            CalendarDaySwitch.IsToggled = settings.ShowCalendarDays;
        }

        //* Event Handlers
        private void CalendarDaySwitch_Toggled(object sender, ToggledEventArgs e) =>
            settings.ShowCalendarDays = CalendarDaySwitch.IsToggled;

        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ThemePicker.SelectedItem as ThemeItem;

            if (item != null)
                settings.Theme = item.Theme;
        }
    }
}