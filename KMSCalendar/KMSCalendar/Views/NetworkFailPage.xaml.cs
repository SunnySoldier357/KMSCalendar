using System;

using KMSCalendar.Services.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetworkFailPage : ContentPage
    {
        //* Private Properties
        private DataOperation operation;

        //* Constructors
        public NetworkFailPage(DataOperation operation)
        {
            InitializeComponent();

            this.operation = operation;
        }

        //* Event Handlers
        private async void RetryButton_Clicked(object sender, EventArgs e)
        {
            if (operation.TryToGetData())
            {
                operation.waitHandle.Set();
                await Navigation.PopModalAsync();
            }
        }
    }
}