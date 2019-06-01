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
	public partial class SelectPeriodView : ContentView
	{
        public ClassSearchPage parentPage;

        public List<string> periods;

        public SelectPeriodView()
        {
            InitializeComponent();

            periods = new List<string>
            {
                "Period 0",
                "Period 1",
                "Period 2",
                "Period 3",
                "Period 4",
                "Period 5",
                "Period 6",
                "Period 7",
                "Period 8",
            };

            ListOfPeriods.ItemsSource = periods;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            parentPage.swap();
        }

        private async Task DoneButton_ClickedAsync(object sender, EventArgs e)
        {
            await parentPage.GoToCalendarAsync();
        }
    }
}