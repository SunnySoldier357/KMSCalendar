using System;
using System.Collections.Generic;
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

        //* Constructor
        public SettingsPage()
        {
            InitializeComponent();

            //List<ThemeItem> pickerItems = new List<ThemeItem>
            //{
            //    new ThemeItem
            //    {
            //        Name = nameof(Theme.Light),
            //        Theme = Theme.Light
            //    },
            //    new ThemeItem
            //    {
            //        Name = nameof(Theme.Dark),
            //        Theme = Theme.Dark
            //    }
            //};

            //ThemePicker.ItemsSource = pickerItems;
            //ThemePicker.SelectedItem = pickerItems.First(a => a.Theme == settings.Theme);

            if (settings.Theme == Theme.Dark)
                ThemeSwitch.IsToggled = true;

            CalendarDaySwitch.IsToggled = settings.ShowCalendarDays;
        }

        //* Event Handlers
        private void CalendarDaySwitch_Toggled(object sender, ToggledEventArgs e) =>
            settings.ShowCalendarDays = CalendarDaySwitch.IsToggled;

        /*private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeItem item = ThemePicker.SelectedItem as ThemeItem;

            if (item != null)
                settings.Theme = item.Theme;
        }*/

        private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if(ThemeSwitch.IsToggled)
            {
                settings.Theme = Theme.Dark;
            }
            else
            {
                settings.Theme = Theme.Light;
            }
        }
    }
}