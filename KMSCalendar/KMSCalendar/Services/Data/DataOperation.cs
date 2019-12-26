using KMSCalendar.Models.Data;
using KMSCalendar.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KMSCalendar.Services.Data
{
    public class DataOperation
    {
        private Func<string, User> function;  //function
        private string param;  //parameter
        private User data;  //data

        public async Task<User> ConnectToBackendAsync<T2, T1>(Func<string, User> function, string param)
        {
            this.function = function;
            this.param = param;

            if (TryToGetData())
                return data;

            //Waits until the Network FailPage is closed before continuing
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var modalPage = new NetworkFailPage(this);
            modalPage.Disappearing += (sender2, e2) =>              //Found this on stack overflow, still trying to undersand this line.
            {
                waitHandle.Set();
            };
            await (Application.Current as App).MainPage.Navigation.PushModalAsync(modalPage);
            System.Diagnostics.Debug.WriteLine("The modal page is now on screen, hit back button");
            await Task.Run(() => waitHandle.WaitOne());
            System.Diagnostics.Debug.WriteLine("The modal page is dismissed, do something now");

            return data;
        }



        //Generic version that does not work yet:

        //public async Task<T2> ConnectToBackendAsync<T2, T1>(Func<T1, T2> function, T1 param)
        //{
        //    this.function = function;
        //    this.param = param;
        //    r = function.Method.ReturnType;

        //   data = function(param);

        //    if (data != null)
        //        return data;


        //    //Waits until the Network FailPage is closed before continuing
        //    var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        //    var modalPage = new NetworkFailPage(this);
        //    modalPage.Disappearing += (sender2, e2) =>              //Found this on stack overflow, still trying to undersand this line.
        //    {
        //        waitHandle.Set();
        //    };
        //    await (Application.Current as App).MainPage.Navigation.PushModalAsync(modalPage);
        //    System.Diagnostics.Debug.WriteLine("The modal page is now on screen, hit back button");
        //    await Task.Run(() => waitHandle.WaitOne());
        //    System.Diagnostics.Debug.WriteLine("The modal page is dismissed, do something now");

        //    return data;
        //}


        /// <summary>
        /// Attempts to get data from the database, updates the data field if possible.
        /// </summary>
        /// <returns>Whether or not the database access was successful. (bool)</returns>
        public bool TryToGetData()
        {
            try
            {
                data = function(param);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
