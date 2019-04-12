using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using KMSCalendar.Models;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        //* Public Properties
        public Command LoadItemsCommand { get; set; }

        public ObservableCollection<Assignment> Assignments { get; set; }
        
        //* Constructors */
        public AssignmentViewModel()
        {
            Title = "Assignments Calendar";
            Assignments = new ObservableCollection<Assignment>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewAssignmentPage, Assignment>(this,
                "AddAssignment", async (page, a) =>
            {
                Assignment newItem = a as Assignment;
                Assignments.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Assignments.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                    Assignments.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}