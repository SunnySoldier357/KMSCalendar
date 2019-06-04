using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAssignmentPage : ContentPage
    {
        //* Private Properties
        private NewAssignmentViewModel viewModel;

        //* Contructors
        public NewAssignmentPage()
        {
            BindingContext = viewModel = new NewAssignmentViewModel();

            var names = new List<string>();
            names.Add("wow");
            names.Add("who");
            names.Add("why");

            ClassPicker.ItemsSource = names;

            InitializeComponent();
            // TODO: MATEO add a dropdown for people to choose a class the assignment belongs to

            // TODO: MATEO add a button to got to the search for class if the class isn't there
        }

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
            MessagingCenter.Send(this, "AddAssignment", viewModel.Assignment);
            await Navigation.PopModalAsync();
        }

        
    }
}