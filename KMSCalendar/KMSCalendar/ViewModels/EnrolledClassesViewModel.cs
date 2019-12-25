using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
    class EnrolledClassesViewModel : BaseViewModel
    {

       private List<Class> classes;

       public List<Class> Classes
        {
            get => classes;
            set => setProperty(ref classes, value);
        }

        public ICommand UnsubscribeCommand { get; set; }
        public ICommand UnsubscribeButtonClicked { get; set; }

        public EnrolledClassesViewModel()
        {
            UpdateData();

            UnsubscribeButtonClicked = new Command((object item) =>
            {
                Class @class = item as Class;
                @class.UserId = (Application.Current as App).SignedInUser.Id;
                ExecuteUnsubscribeCommand(@class);
            });

            MessagingCenter.Subscribe<ClassSearchViewModel>(this, "UpdateClasses",
                (sender) => UpdateData());
        }

        public void UpdateData()
        {
            (Application.Current as App).PullEnrolledTeachers();
            Classes = (Application.Current as App).SignedInUser.EnrolledClasses;
        }

        public void ExecuteUnsubscribeCommand(Class @class)
        {
            ClassManager.UnenrollUserFromClass(@class);

            (Application.Current as App).PullEnrolledClasses();
            UpdateData();
            MessagingCenter.Send(this, "LoadAssignments");
            //Todo: If there are no other users in the class, delete the class
        }
    }
}
