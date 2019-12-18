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
        public ICommand ReloadButtonClicked { get; set; }

        public EnrolledClassesViewModel()
        {
            UpdateData();

            ReloadButtonClicked = new Command((object item) =>
            {
                //Todo: fix this goofy way of getting the class Id
                var label = item as Label;
                Guid classId = new Guid(label.Text);
                foreach(Class @class in Classes)
                {
                    if (@class.Id == classId)
                    {
                        @class.UserId = (Application.Current as App).SignedInUser.Id;
                        ExecuteUnsubscribeCommand(@class);
                    }
                }
            });

                //UnsubscribeCommand = new Command(async () => await ExecuteUnsubscribeCommand());

            MessagingCenter.Subscribe<ClassSearchPage>(this, "UpdateClasses",
                (sender) => UpdateData());
        }

        public void UpdateData()
        {
            (Application.Current as App).PullEnrolledTeachers();
            Classes = (Application.Current as App).SignedInUser.EnrolledClasses;
        }

        public void ExecuteUnsubscribeCommand(Class @class)
        {
            ClassManager.RemoveClassUser(@class);

            (Application.Current as App).PullEnrolledClasses();
            UpdateData();
            //Todo: If there are no other users in the class, delete the class
        }

        
    }

}
