using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAssignmentPage : ContentPage
    {
        //* Private Properties
        private NewAssignmentViewModel viewModel;

        //* Contructors
        public NewAssignmentPage() : this(DateTime.Today) { }

        /// <summary>
        /// This constructor is used with the date selected on the calendar.
        /// </summary>
        /// <param name="dateSelected"></param>
        public NewAssignmentPage(DateTime dateSelected)
        {
            InitializeComponent();

            BindingContext = viewModel = new NewAssignmentViewModel(dateSelected);
        }

        //* Event Handlers
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        public async void Save_Clicked(object sender, EventArgs e)
        {
            var data = ClassPicker.SelectedItem;
            // TODO: SUNNY get the class selected for the assignment

            MessagingCenter.Send(this, "AddAssignment", viewModel.Assignment);
            await Navigation.PopModalAsync();
        }

        private void GoToSearchButton_Clicked(object sender, EventArgs e) =>
            Navigation.PushAsync(new ClassSearchPage());
    }
}