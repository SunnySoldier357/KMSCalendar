using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;
using System.Linq;
using KMSCalendar.Services.Data;
using System.Threading.Tasks;
namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
        //* Private Properties
        private IDataStore<Teacher> teacherDataStore;
        private IDataStore<Class> classDataStore;

        //* Public Properties
        public NewClassViewModel ViewModel;

        //* Constructors
		public NewClassPage(Page parentPage)
		{
            InitializeComponent();
            BindingContext = ViewModel= new NewClassViewModel();

            teacherDataStore = DependencyService.Get<IDataStore<Teacher>>();   //this has no item
            classDataStore = DependencyService.Get<IDataStore<Class>>();

            LoadTeachers();
        }

        public async void LoadTeachers()
        {
            var teachers = await teacherDataStore.GetItemsAsync(true);

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
        private async Task authenticateAsync()
        {
                //If a new teacher is written into the box:
            if (ViewModel.TeacherName != null && ViewModel.ClassName != null && ViewModel.TeacherName != "")
            {
                Teacher t = new Teacher { Name = ViewModel.TeacherName };

                await addTeacherAsync(t);
            }
            else if (ViewModel.SelectedTeacher != null && ViewModel.ClassName != null)      //Otherwise if a teacher is selected
            {
                addClass(ViewModel.ClassName, ViewModel.Period, ViewModel.SelectedTeacher);
            }
        }

        //TODO: MATEO get the teacher with id and add it to the add class method
        private async Task addTeacherAsync(Teacher t)
        {
            await teacherDataStore.AddItemAsync(t);      //adds the teacher to the database

            addClass(ViewModel.ClassName, ViewModel.Period, t);
        }

        /// <summary>
        /// Adds new class to database and then goes back to the class search page
        /// </summary>
        private async void addClass(string className, int period, Teacher teacher)
        {
            Class newClass = new Class
            {
                Name = className,
                Period = period,
                Teacher = teacher
            };
               
            await classDataStore.AddItemAsync(newClass);        //this only works if the class's Teacher value is null

            //Navigates back to the class search page
            await Navigation.PopModalAsync();
        }
    }
}