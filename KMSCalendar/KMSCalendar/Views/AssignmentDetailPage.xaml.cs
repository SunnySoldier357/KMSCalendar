using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Entities;
using KMSCalendar.ViewModels;
using KMSCalendar.Services;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentDetailPage : ContentPage
    {
        //* Private Properties
        private AssignmentDetailViewModel viewModel;

        //* Constructors
        public AssignmentDetailPage()
        {
            InitializeComponent();

            Assignment assignment = new Assignment
            {
                Name = "Item 1",
                Description = "This is an item description",
                DueDate = DateTime.Today
            };

            BindingContext = viewModel = new AssignmentDetailViewModel(assignment);
        }

        public AssignmentDetailPage(AssignmentDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        //* Event Handlers
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            IDataStore<Assignment> data = DependencyService.Get<IDataStore<Assignment>>();
            await data.DeleteItemAsync(viewModel.Assignment.Id);

            await Navigation.PushAsync(new AssignmentsPage());
        }

        //TODO: MATEO add a dropdown for people to choose a class the assignment belongs to

        //TODO: MATEO add a button to got to the search for class if the class isn't there
    }
}