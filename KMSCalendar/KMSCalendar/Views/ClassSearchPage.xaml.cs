using KMSCalendar.Models.Entities;
using KMSCalendar.Services;
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
	public partial class ClassSearchPage : ContentPage
	{
        private List<Class> classList;

        private SelectPeriodView selectPeriodView;

        public ClassSearchPage()
        {
            InitializeComponent();

            selectPeriodView = new SelectPeriodView();
            SelectPeriods.parentPage = this;
            popUpGrid.IsVisible = false;

            LoadList();

            //Summary: Event handleres for the searchbar text changing or the searchbutton is pressed.
            sbSearch.TextChanged += (sender2, e2) => FilterContacts(sbSearch.Text);
            sbSearch.SearchButtonPressed += (sender2, e2) => FilterContacts(sbSearch.Text);
        }

        /// <summary>
        /// Loads the list with data before the user searches anything.
        /// </summary>
        private async void LoadList()
        {
            var data = DependencyService.Get<IDataStore<Class>>();
            var tempClassList = await data.GetItemsAsync();
            classList = tempClassList.ToList();
            ClassesLv.ItemsSource = classList;

            //_lstContacts = modDatabase.GetContacts(string.Empty); 
            //lvAddressBook.ItemsSource = _lstContacts;
        }

        /// <summary>
        /// Filters the listview below the searchbar based off of what the user types in.
        /// </summary>
        /// <param name="filter"> The search term that the user puts in </param>
        private async void FilterContacts(string filter)
        {
            if(filter == "hide")
            {
                swap();
            }

            //    lvAddressBook.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                ClassesLv.ItemsSource = classList;
                //lvAddressBook.ItemsSource = _lstContacts;
            }
            else
            {
                //Summary: filter the data list for only items what contain the search term.
                ClassesLv.ItemsSource = classList.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            }
            ClassesLv.EndRefresh();
        }

        public void swap()
        {
            if(SearchArea.IsVisible)
            {
                SearchArea.IsVisible = false;
                popUpGrid.IsVisible = true;
            }
            else
            {
                popUpGrid.IsVisible = false;
                SearchArea.IsVisible = true;
            }
        }

        public async Task GoToCalendarAsync()
        {
            //TODO: MATEO make it actually go to the calendar
            //TODO: SUNNY add the peiod selected and class selected to the database.

            //true makes it animated
            await Navigation.PushAsync(new AssignmentsPage(), true);
        }

        private void ClassesLv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            swap();
        }
    }
}