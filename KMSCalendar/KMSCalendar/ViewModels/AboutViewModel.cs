using System;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        //* Public Properties
        public ICommand OpenWebCommand { get; }

        //* Constructors
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(async () =>
                await Launcher.OpenAsync(new Uri("https://xamarin.com/platform")));
        }
    }
}