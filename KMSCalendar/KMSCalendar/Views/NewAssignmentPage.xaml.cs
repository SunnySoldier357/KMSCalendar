using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Entities;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAssignmentPage : ContentPage
    {
        //* Public Properties
        public Assignment Assignment { get; set; }

        //* Contructors
        public NewAssignmentPage()
        {
            InitializeComponent();

            Assignment = new Assignment
            {
                Name = "Assignment name",
                Description = "This is an item description",
                DueDate = DateTime.Today
            };

            BindingContext = this;
        }

        /// <summary>
        /// This constructor is used with the date selected on the calendar.
        /// </summary>
        /// <param name="dateSelected"></param>
        public NewAssignmentPage(DateTime dateSelected)
        {
            InitializeComponent();

            Assignment = new Assignment
            {
                Name = "",
                Description = "",
                DueDate = dateSelected
            };

            BindingContext = this;
        }



        //* Event Handlers
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddAssignment", Assignment);
            await Navigation.PopModalAsync();
        }
    }
}