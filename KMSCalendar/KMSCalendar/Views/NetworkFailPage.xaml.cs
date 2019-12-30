﻿using System;
using System.Threading.Tasks;
using KMSCalendar.Services.Data;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkFailPage : ContentPage
	{
		//* Private Properties
		private DataOperation operation;

		public NetworkFailPage() => InitializeComponent();

		//* Constructors
		public NetworkFailPage(DataOperation operation)
		{
			InitializeComponent();

			this.operation = operation;
		}

		//* Event Handlers
		private async void RetryButton_Clicked(object sender, EventArgs e)
		{
			LoadingAnimation.IsLoading = true;
			await Task.Run(async () =>
			{
				if (operation != null)
				{
					if (operation.TryToGetData())
					{
						operation.waitHandle.Set();
					}
				}
				else
					await Task.Delay(3000);

				if (Connectivity.NetworkAccess == NetworkAccess.Internet)
				{
					// Connection to internet is available
					await Navigation.PopModalAsync();
				}
			});
			LoadingAnimation.IsLoading = false;
		}
	}
}