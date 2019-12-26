using KMSCalendar.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetworkFailPage : ContentPage
    {
        private DataOperation operation;

        public NetworkFailPage(DataOperation operation)
        {
            InitializeComponent();

            this.operation = operation;
        }

        private async void RetryButton_Clicked(object sender, EventArgs e)
        {
            if (operation.TryToGetData())
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}