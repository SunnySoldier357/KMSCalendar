﻿using System;
using System.Windows.Input;

using PropertyChanged;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[DoNotNotify]
	public class AboutViewModel : BaseViewModel
	{
		//* Public Properties
		public ICommand OpenWebCommand { get; }

		//* Constructors
		public AboutViewModel()
		{
			OpenWebCommand = new Command(async () =>
				await Launcher.OpenAsync(new Uri("https://xamarin.com/platform")));
		}
	}
}