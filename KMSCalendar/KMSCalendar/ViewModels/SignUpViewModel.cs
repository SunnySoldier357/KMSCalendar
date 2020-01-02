using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

using ModelValidation;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class SignUpViewModel : LogInViewModel
	{
		//* Private Properties
		private string confirmPassword;
		private string userName;

		private string zipCode;
		private List<School> filteredSchoolList;
		private School selectedSchool;

		private string name;

		private bool signUpVisibility;
		private bool schoolEnrollmentVisibility;
		private bool newSchoolVisibility;

		//* Public Properties
		public ICommand AlreadyUserCommand { get; set; }
		public new ICommand AuthenticateUserCommand { get; set; }
		public ICommand CreateUserCommand { get; set; }
		public ICommand FilterZipCodesCommand { get; set; }
		public ICommand NewSchoolViewCommand { get; set; }
		public ICommand GoBackCommand { get; set; }
		public ICommand AddNewSchoolCommand { get; set; }

		[PropertyValueMatch(nameof(Password),
			ErrorMessage = "The Passwords do not match!")]
		public string ConfirmPassword
		{
			get => confirmPassword;
			set => setProperty(ref confirmPassword, value);
		}
		[MinimumLength(2)]
		[MaximumLength(64)]
		public string UserName
		{
			get => userName;
			set => setProperty(ref userName, value);
		}

		public string ZipCode
		{
			get => zipCode;
			set => setProperty(ref zipCode, value);
		}
		public List<School> SchoolList { get; set; }
		public List<School> FilteredSchoolList
		{
			get => filteredSchoolList;
			set => setProperty(ref filteredSchoolList, value);
		}
		public string Name
		{
			get => name;
			set => setProperty(ref name, value);
		}
		public School SelectedSchool
		{
			get => selectedSchool;
			set => setProperty(ref selectedSchool, value);
		}

		public bool SignUpVisibility
		{
			get => signUpVisibility;
			set => setProperty(ref signUpVisibility, value);
		}
		public bool SchoolEnrollmentVisibility
		{
			get => schoolEnrollmentVisibility;
			set => setProperty(ref schoolEnrollmentVisibility, value);
		}
		public bool NewSchoolVisibility
		{
			get => newSchoolVisibility;
			set => setProperty(ref newSchoolVisibility, value);
		}


		//* Constructors
		public SignUpViewModel() : base()
		{
			Title = "Sign Up";

			SignUpVisibility = true;
			SchoolEnrollmentVisibility = false;

			ConfirmPassword = string.Empty;
			UserName = string.Empty;

			AlreadyUserCommand = new Command(() => ExecuteAlreadyUserCommand());
			AuthenticateUserCommand = new Command(async () => await ExecuteAuthenticateUserCommandAsync());
			CreateUserCommand = new Command(async () => await SignUpUserAsync());
			FilterZipCodesCommand = new Command(() => FilterData(ZipCode));
			NewSchoolViewCommand = new Command(() => GoToNewSchoolView());
			GoBackCommand = new Command(() => GoBack());
			AddNewSchoolCommand = new Command(async () => await AddNewSchoolAsync());
		}

		//* Public Methods
		public void ExecuteAlreadyUserCommand() =>
			App.MainPage = new LogInPage();

		public async Task ExecuteAuthenticateUserCommandAsync()
		{
			await Task.Run(async () =>
			{
				if (Validate())
				{
					//True if the user may use this email to sign up
					bool newEmail = !DataOperation.ConnectToBackend(UserManager.CheckForUser, Email.Trim());

					if (!newEmail)
						LoginValidationMessage = "User already exists! Please log in instead.";
					else
						await SwapViewsAsync();
				}
			});
		}

		/// <summary>
		/// filters the list of teachers only if the teachers name contains the searchbar term entered
		/// </summary>
		/// <param name="filter">The searchbar term entered</param>
		private void FilterData(string filter)
		{
			if (string.IsNullOrWhiteSpace(filter))
				FilteredSchoolList = SchoolList;
			else
				FilteredSchoolList = SchoolList.Where(x => x.ZipCode.ToLower().Contains(filter.ToLower())).ToList();
		}

		private async Task AddNewSchoolAsync()
		{
			var school = new School
			{
				Name = Name,
				ZipCode = ZipCode,
			};

			await Task.Run(() => DataOperation.ConnectToBackend(SchoolManager.AddSchool, school));

			SchoolList.Add(school);
			FilterData(ZipCode);
			GoBack();
		}


		private async Task SignUpUserAsync()
		{
			if (SelectedSchool != null)
			{
				string hashedPassword = PasswordHasher.HashPassword(Password);

				var user = new User
				{
					Id = Guid.NewGuid(),
					Email = Email.Trim(),
					UserName = UserName.Trim(),
					Password = hashedPassword,
					SchoolId = SelectedSchool.Id
				};

				await Task.Run(() => DataOperation.ConnectToBackend(UserManager.AddUser, user));

				App.SignedInUser = user;
				UserSettings.SignedInUserId = user.Id;

				App.MainPage = new MainPage();
			}
		}

		private async Task SwapViewsAsync()
		{
			LoginValidationMessage = "";
			if (SignUpVisibility)
			{
				SignUpVisibility = false;
				SchoolEnrollmentVisibility = true;
				await Task.Run(() => SchoolList = DataOperation.ConnectToBackendWithoutParam(SchoolManager.LoadSchools) ??
					new List<School>());
				FilteredSchoolList = SchoolList;
			}
			else
			{
				SchoolEnrollmentVisibility = false;
				SignUpVisibility = true;
			}
		}
		private void GoToNewSchoolView()
		{
			SchoolEnrollmentVisibility = false;
			NewSchoolVisibility = true;
		}
		private void GoBack()
		{
			NewSchoolVisibility = false;
			SchoolEnrollmentVisibility = true;
		}
	}
}