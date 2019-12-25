using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.ViewModels;
using System.Diagnostics;

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

            MessagingCenter.Subscribe<ClassSearchViewModel>(this, "LoadClassesForNewAssignmentPage",
                (sender) => viewModel.LoadSubscribedClasses());
        }

        //* Event Handlers
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        public async void Save_Clicked(object sender, EventArgs e)
        {
            if(ClassPicker.SelectedItem != null && viewModel.Assignment.Name != "" && viewModel.Assignment.Description != null)
            {
                var selectedClass = ClassPicker.SelectedItem;
                viewModel.Assignment.Class = selectedClass as Models.Data.Class;    //sets the viewModel's assignment to the class selected from the picker
            
                MessagingCenter.Send(this, "AddAssignment", viewModel.Assignment);

                await Navigation.PopModalAsync();
            }
        }

        private void GoToSearchButton_Clicked(object sender, EventArgs e) =>
            Navigation.PushModalAsync(new ClassSearchPage());
    }
}