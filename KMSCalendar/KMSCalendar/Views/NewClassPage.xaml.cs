﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
		private App app = (Application.Current as App);

		//* Public Properties
		public NewClassViewModel ViewModel;

		//* Constructors
		public NewClassPage(Page parentPage)
		{
			InitializeComponent();

			BindingContext = ViewModel = new NewClassViewModel();
		}

		//public void LoadTeachers()
		//{
		//	List<Teacher> teachers = TeacherManager.LoadAllTeachers(app.SignedInUser.SchoolId);

		//	ViewModel.Teachers = teachers.ToList();

		//	TeachersListView.ItemsSource = ViewModel.Teachers;
		//}

		//* Event Handlers
		private void TeacherSearchBar_TextChanged(object sender, TextChangedEventArgs e) => FilterData(ViewModel.SearchTerm);
		/// <summary>
		/// filters the list of teachers only if the teachers name contains the searchbar term entered
		/// </summary>
		/// <param name="filter">The searchbar term entered</param>
		private void FilterData(string filter)
		{
			TeachersListView.BeginRefresh();
			if (string.IsNullOrWhiteSpace(filter))
			{
				TeachersListView.ItemsSource = ViewModel.Teachers;
			}
			else
			{
				TeachersListView.ItemsSource = ViewModel.Teachers.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
			}
			TeachersListView.EndRefresh();
		}

		private void TeachersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			// Set the teacher selected
			ViewModel.SelectedTeacher = TeachersListView.SelectedItem as Teacher;
		}

		/// <summary>
		/// These next two buttons should do the same thing and add a new class (and possibly teacher) to the database with the authenticate method
		/// </summary>
		private async void DoneButton_Clicked(object sender, EventArgs e) =>
			await authenticateAsync();

		private async void NextButton_Clicked(object sender, EventArgs e) =>
			await authenticateAsync();

		private void BackButton_Clicked(object sender, EventArgs e) =>
			Navigation.PopModalAsync();

		/// <summary>
		/// If the viewModel data is valid, it will go to the addClass() method
		/// </summary>
		private async Task authenticateAsync()
		{
			if (int.TryParse(ViewModel.Period, out int newPeriod))
			{
				// If a new teacher is written into the box:
				if (!string.IsNullOrEmpty(ViewModel.TeacherName) && ViewModel.ClassName != null)
				{
					var t = new Teacher
					{
						Id = Guid.NewGuid(),
						Name = ViewModel.TeacherName,
						SchoolId = app.SignedInUser.SchoolId
					};

					ViewModel.DataOperation.ConnectToBackend(TeacherManager.AddTeacher, t);

					addClass(ViewModel.ClassName, newPeriod, t.Id, t.SchoolId);
				}

				// Otherwise if a teacher is selected
				else if (ViewModel.SelectedTeacher != null && ViewModel.ClassName != null)
					addClass(ViewModel.ClassName, newPeriod, ViewModel.SelectedTeacher.Id, ViewModel.SelectedTeacher.SchoolId);

				MessagingCenter.Send<NewClassPage>(this, "LoadClasses");

				await Navigation.PopModalAsync();
			}
		}



		/// <summary>
		/// Adds new class to database and then goes back to the class search page
		/// </summary>
		private async void addClass(string className, int period, Guid teacherId, Guid schoolId)
		{
			var newClass = new Class
			{
				Id = Guid.NewGuid(),
				Name = className,
				Period = period,
				TeacherId = teacherId,
				UserId = app.SignedInUser.Id,
				SchoolId = schoolId
			};

			ViewModel.DataOperation.ConnectToBackend(ClassManager.AddClass, newClass);  //Adds class and new period to the db

			//Navigates back to the class search page
			await Navigation.PopModalAsync();
		}

		private string getSchoolName(Guid schoolId) => ViewModel.DataOperation.ConnectToBackend(SchoolManager.GetSchoolName, schoolId);
	}
}