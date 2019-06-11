using System;

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

        //* Constructors
        public SelectPeriodControl() => 
            InitializeComponent();

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

        public void OnLoaded(object sender, EventArgs e)
        {
            if (PeriodsListView.BindingContext == null)
                PeriodsListView.BindingContext = ParentPage.ViewModel;

            ParentPage.ViewModel.LoadPeriods();
        }

        //TODO: MATEO add the option to add a new period
    }
}