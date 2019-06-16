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

            //TODO: MATEO get the filtering of the teachers to work.

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
        private void TeachersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Set the teacher selected
            ViewModel.SelectedTeacher = TeachersListView.SelectedItem as Teacher;
        }

        /// <summary>
        /// If the viewModel data is valid, it will go to the addClass() method
        /// </summary>
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            if(ViewModel.TeacherName != null && ViewModel.ClassName != null)
            {
                Teacher t = new Teacher { Name = ViewModel.TeacherName };
                addClass(ViewModel.ClassName, ViewModel.Period, t);
            }
            else if(ViewModel.SelectedTeacher != null && ViewModel.ClassName != null)
            {
                addClass(ViewModel.ClassName, ViewModel.Period, ViewModel.SelectedTeacher);
            }
        }

        /// <summary>
        /// Goes back to the class search page
        /// </summary>
        private void addClass(string className, int period, Teacher teacher)
        {
            Class newClass = new Class
            {
                Name = className,
                Period = period,
                Teacher = teacher
            };

            //TODO: SUNNY add the new class to the database

            //Navigates back to the class search page
            var MyAppsFirstPage = new ClassSearchPage();
            Application.Current.MainPage = new NavigationPage(MyAppsFirstPage);
            Application.Current.MainPage.Navigation.PushAsync(new ClassSearchPage());
            Application.Current.MainPage.Navigation.PopAsync();         // Remove the page currently on top.
        }
    }
}