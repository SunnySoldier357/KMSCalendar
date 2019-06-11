using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
        //* Public Properties

        // public Page ParentPage;

        public NewClassViewModel ViewModel;

        //* Constructors
		public NewClassPage(Page parentPage)
		{
            // ParentPage = parentPage;

            InitializeComponent();
            BindingContext = ViewModel= new NewClassViewModel();
		}

        //* Event Handlers

        /// <summary>
        /// Goes back to the class search page
        /// </summary>
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            // TODO: SUNNY: Add the new class to the database

            var MyAppsFirstPage = new ClassSearchPage();
            Application.Current.MainPage = new NavigationPage(MyAppsFirstPage);

            Application.Current.MainPage.Navigation.PushAsync(new ClassSearchPage());

            // Remove the page currently on top.
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void TeachersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Get the teacher selected
        }
    }
}