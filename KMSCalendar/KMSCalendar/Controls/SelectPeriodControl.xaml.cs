using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Views;

namespace KMSCalendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPeriodControl : ContentView
    {
        //* Public Properties
        public ClassSearchPage ParentPage;

        public List<int> Periods;

        //* Constructors
        public SelectPeriodControl()
        {
            InitializeComponent();

            Periods = Enumerable.Range(0, 10).ToList();
            PeriodsListView.ItemsSource = Periods;
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