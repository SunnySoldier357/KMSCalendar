using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentDetailPage : ContentPage
    {
        //* Constructors
        public AssignmentDetailPage(Assignment assignment)
        {
            InitializeComponent();

            BindingContext = new AssignmentDetailViewModel(assignment);
        }

        //* Event Handlers
        private async void Delete_Clicked(object sender, EventArgs e) => 
            await Navigation.PushAsync(new AssignmentsPage());
    }
}