using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Views;
using System.Linq;

namespace KMSCalendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPeriodControl : ContentView
    {
        //* Public Properties
        public ClassSearchPage ParentPage;

        //* Private Properties
        private List<int> periods;

        //* Constructors
        public SelectPeriodControl()
        {
            InitializeComponent();

            periods = Enumerable.Range(0, 10).ToList();

            PeriodsListView.ItemsSource = periods;
        }

        //* Event Handlers
        private void BackButton_Clicked(object sender, EventArgs e) => 
            ParentPage.Swap();

        private async void DoneButton_Clicked(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedItem != null)
            {
                int periodChosen = (int) PeriodsListView.SelectedItem;

                await ParentPage.GoToCalendarAsync(periodChosen);
            }
        }
    }
}