using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.ViewModels;

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
    }
}