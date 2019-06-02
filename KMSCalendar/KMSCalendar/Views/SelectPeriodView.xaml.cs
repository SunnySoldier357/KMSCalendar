using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<int> periods;

        public SelectPeriodView()
        {
            InitializeComponent();

            periods = new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };

            ListOfPeriods.ItemsSource = periods;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            parentPage.swap();
        }

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            if(ListOfPeriods.SelectedItem != null)
            {
                int periodChosen = (int)ListOfPeriods.SelectedItem;

                parentPage.GoToCalendarAsync(periodChosen);
            }

        }


    }
}