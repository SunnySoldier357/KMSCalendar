using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;
using System.Linq;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
        //* Public Properties
        public NewClassViewModel ViewModel;

        //* Constructors
		public NewClassPage(Page parentPage)
		{
            InitializeComponent();
            BindingContext = ViewModel= new NewClassViewModel();
		}

        //* Event Handlers 
        private void TeacherSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData(ViewModel.SearchTerm);
        }
        /// <summary>
        /// filters the list of teachers only if the teachers name contains the searchbar term entered
        /// </summary>
        /// <param name="filter">The searchbar term entered</param>
        private void FilterData(string filter)
        {
            TeachersListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                TeachersListView.ItemsSource = ViewModel.Teachers;
            }
            else
            {
                TeachersListView.ItemsSource = ViewModel.Teachers.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            }
            TeachersListView.EndRefresh();
        }

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

                //TODO: SUNNY add the new teacher to the database

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