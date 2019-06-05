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
        public NewClassViewModel viewModel;

		public NewClassPage ()
		{
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

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            //TODO: MATEO go back to the class selection page
        }

        private void TeachersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //get the teacher selected
        }
    }
}