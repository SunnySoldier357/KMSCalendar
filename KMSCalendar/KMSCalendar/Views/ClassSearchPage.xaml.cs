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

        public ClassSearchPage()
        {
            InitializeComponent();

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
	}
}