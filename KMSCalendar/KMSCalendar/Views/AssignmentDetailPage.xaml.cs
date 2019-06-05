using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Entities;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentDetailPage : ContentPage
    {
        //* Private Properties
        private AssignmentDetailViewModel viewModel;

        //* Constructors
        public AssignmentDetailPage(Assignment assignment)
        {
            InitializeComponent();

            BindingContext = viewModel = new AssignmentDetailViewModel(assignment);
        }

        //* Event Handlers
        private async void Delete_Clicked(object sender, EventArgs e) => 
            await Navigation.PushAsync(new AssignmentsPage());
    }
}