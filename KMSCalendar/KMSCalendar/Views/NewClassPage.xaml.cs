using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;
using System.Linq;
using KMSCalendar.Services.Data;
using System.Diagnostics;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
        //* Private Properties
        private IDataStore<Teacher> dataStore;

        //* Public Properties
        public NewClassViewModel ViewModel;

        //* Constructors
		public NewClassPage(Page parentPage)
		{
            InitializeComponent();
            BindingContext = ViewModel= new NewClassViewModel();

            dataStore = DependencyService.Get<IDataStore<Teacher>>();   //this has no item

            LoadTeachers();
        }

        public async void LoadTeachers()
        {
            var teachers = await dataStore.GetItemsAsync(true);

            ViewModel.Teachers = teachers.ToList();

            TeachersListView.ItemsSource = ViewModel.Teachers;
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
        /// These next two buttons should do the same thing and add a new class (and possibly teacher) to the database with the authenticate method
        /// </summary>
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            authenticateAsync();
        }
        private void NextButton_Clicked(object sender, EventArgs e)
        {
            authenticateAsync();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// If the viewModel data is valid, it will go to the addClass() method
        /// </summary>
        private async System.Threading.Tasks.Task authenticateAsync()
        {
                //If a new teacher is written into the box:
            if (ViewModel.TeacherName != null && ViewModel.ClassName != null && ViewModel.TeacherName != "")
            {
                Teacher t = new Teacher { Name = ViewModel.TeacherName };

                //TODO: SUNNY add the new teacher to the database
                await addTeacherAsync(t);
            }
            else if (ViewModel.SelectedTeacher != null && ViewModel.ClassName != null)      //Otherwise if a teacher is selected
            {
                addClass(ViewModel.ClassName, ViewModel.Period, ViewModel.SelectedTeacher);
            }
        }

        //TODO: MATEO get the teacher with id and add it to the add class method
        private async System.Threading.Tasks.Task addTeacherAsync(Teacher t)
        {
            await dataStore.AddItemAsync(t);      //adds the teacher to the database

            addClass(ViewModel.ClassName, ViewModel.Period, t);
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
            Navigation.PopModalAsync();
        }
    }
}