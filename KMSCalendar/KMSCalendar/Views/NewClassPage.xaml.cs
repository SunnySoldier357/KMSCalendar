using KMSCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
        //public Page parentPage;

        public NewClassViewModel viewModel;

		public NewClassPage(Page parentPage)
		{
            //this.parentPage = parentPage;

            viewModel = new NewClassViewModel();
            Models.Data.Teacher t = new Models.Data.Teacher();
            t.Name = "Mr. Parker";
            viewModel.Teachers.Add(t);

            Models.Data.Teacher t1 = new Models.Data.Teacher();
            t1.Name = "Ms. Boas";
            viewModel.Teachers.Add(t1);

            InitializeComponent ();
            BindingContext = viewModel;
		}

        /// <summary>
        /// Goes back to the class search page
        /// </summary>
        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            var MyAppsFirstPage = new ClassSearchPage();
            Application.Current.MainPage = new NavigationPage(MyAppsFirstPage);

            Application.Current.MainPage.Navigation.PushAsync(new ClassSearchPage());
            Application.Current.MainPage.Navigation.PopAsync(); //Remove the page currently on top.
        }

        private void TeachersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //get the teacher selected
        }
    }
}