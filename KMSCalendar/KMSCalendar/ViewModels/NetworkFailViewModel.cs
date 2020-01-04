using System.Windows.Input;

using KMSCalendar.Models;
using KMSCalendar.Services.Data;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class NetworkFailViewModel : BaseViewModel
	{
		//* Private Properties
		private DataOperation operation;

		//* Public Properties
		public ICommand RetryConnectionCommand { get; }

		//* Constructors
		public NetworkFailViewModel(DataOperation operation)
		{
			this.operation = operation;

			RetryConnectionCommand = new Command(() => retryConnection());
		}

		//* Private Methods
		private void retryConnection()
		{
			IsBusy = true;

			if (operation?.TryToGetData() ?? false)
				operation.WaitHandle.Set();

			if (Connectivity.NetworkAccess == NetworkAccess.Internet)
			{
				// Connection to internet is available
				MessagingCenter.Send(this, MessagingEvent.GoBack);
			}

			IsBusy = false;
		}
	}
}