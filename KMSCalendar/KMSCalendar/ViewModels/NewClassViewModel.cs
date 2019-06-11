using System.Collections.Generic;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

namespace KMSCalendar.ViewModels
{
    public class NewClassViewModel : BaseViewModel
    {
        //* Private Properties
        private List<Teacher> teachers;

        //* Public Properties
        public List<Teacher> Teachers
        {
            get => teachers;
            set => setProperty(ref teachers, value);
        }

        //* Constructors
        public NewClassViewModel()
        {
            var dataStore = DependencyService.Get<IDataStore<Teacher>>();
            Teachers = new List<Teacher>(dataStore.GetItemsAsync(false).Result);
        }
    }
}