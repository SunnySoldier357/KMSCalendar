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
        private dynamic function;
        private dynamic param;
        private dynamic data;

        /// <summary>
        /// Attempts to get data from the backend, if it fails, it opens a network fail page.
        /// </summary>
        /// <typeparam name="T1">The type of param. (i.e. Guid)</typeparam>
        /// <typeparam name="T2">The return type of the function.</typeparam>
        /// <param name="function">The function used to get data from the backend.</param>
        /// <param name="param">The parameter passed into the backend function.</param>
        /// <returns>The data retrieved from the database.</returns>
        public async Task<T2> ConnectToBackendAsync<T1, T2>(Func<T1, T2> function, T1 param)
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
            catch { return false; }
        }
    }
}
