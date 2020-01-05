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
		private List<School> schools;
		private List<School> filteredSchools;

		private string confirmPassword;
		private string schoolName;
		private string userName;
		private string zipCode;

		private UIState uiState = UIState.SignUpView;
		private UIState currentUIState
		{
			get => uiState;
			set
			{
				uiState = value;
				OnNotifyPropertyChanged(nameof(NewSchoolVisibility));
				OnNotifyPropertyChanged(nameof(SchoolEnrollmentVisibility));
				OnNotifyPropertyChanged(nameof(SignUpVisibility));
			}
		}

		//* Public Properties
		public bool NewSchoolVisibility => currentUIState == UIState.NewSchoolView;
		public bool SchoolEnrollmentVisibility => currentUIState == UIState.SchoolEnrollmentView;
		public bool SignUpVisibility => currentUIState == UIState.SignUpView;

		public ICommand AddNewSchoolCommand { get; }
		public new ICommand AuthenticateUserCommand { get; }
		public ICommand CreateUserCommand { get; }
		public ICommand FilterZipCodesCommand { get; }
		public ICommand GoBackCommand { get; }
		public ICommand GoToLogInPageCommand { get; }
		public ICommand NewSchoolViewCommand { get; }

		public List<School> FilteredSchools
		{
			get => filteredSchools;
			set => setProperty(ref filteredSchools, value);
		}

		[PropertyValueMatch(nameof(Password),
			ErrorMessage = "The Passwords do not match!")]
		public string ConfirmPassword
		{
			get => confirmPassword;
			set => setProperty(ref confirmPassword, value);
		}
		public string SchoolName
		{
			get => schoolName;
			set => setProperty(ref schoolName, value);
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

		//* Constructors
		public SignUpViewModel() : base()
		{
			ConfirmPassword = string.Empty;
			UserName = string.Empty;

			AddNewSchoolCommand = new Command(async () => await addNewSchoolAsync());
			AuthenticateUserCommand = new Command(async () => await authenticateUserAsync());
			CreateUserCommand = new Command<object>(async selectedSchool =>
				await createUserAsync(selectedSchool));
			FilterZipCodesCommand = new Command(() => filterZipCodes(ZipCode));
			GoBackCommand = new Command(() => currentUIState = UIState.SchoolEnrollmentView);
			GoToLogInPageCommand = new Command(() => goToLogInPage());
			NewSchoolViewCommand = new Command(() => currentUIState = UIState.NewSchoolView);

			_ = loadSchoolsAsync();
		}

		//* Private Methods
		private async Task addNewSchoolAsync()
		{
			var school = new School
			{
				Name = SchoolName,
				ZipCode = ZipCode,
			};

			await Task.Run(() => DataOperation.ConnectToBackend(SchoolManager.AddSchool, school));

			schools.Add(school);
			filterZipCodes(ZipCode);

			currentUIState = UIState.SchoolEnrollmentView;
		}

		private async Task authenticateUserAsync()
		{
			await Task.Run(() =>
			{
				if (Validate())
				{
					// True if the user may use this email to sign up
					bool newEmail = !DataOperation.ConnectToBackend(UserManager.CheckForUser,
						Email.Trim());

					if (!newEmail)
						LoginValidationMessage = "User already exists! Please log in instead.";
					else
						currentUIState = UIState.SchoolEnrollmentView;
				}
			});
		}

		private async Task createUserAsync(object selectedSchool)
		{
			if (selectedSchool is School school)
			{
				string hashedPassword = PasswordHasher.HashPassword(Password);

				var user = new User
				{
					Id = Guid.NewGuid(),
					Email = Email.Trim(),
					UserName = UserName.Trim(),
					Password = hashedPassword,
					SchoolId = school.Id
				};

				await Task.Run(() => DataOperation.ConnectToBackend(UserManager.AddUser, user));

				App.SignedInUser = user;
				UserSettings.SignedInUserId = user.Id;

				App.MainPage = new MainPage();
			}
		}

		private void filterZipCodes(string searchInput)
		{
			if (string.IsNullOrWhiteSpace(searchInput))
				FilteredSchools = new List<School>(schools);
			else
			{
				var result =
					from school in schools.AsParallel()
					where school.ZipCode.ToLower().Contains(searchInput.ToLower())
					select school;

				FilteredSchools = result.ToList();
			}
		}

		private void goToLogInPage() =>
			App.MainPage = new LogInPage();

		private async Task loadSchoolsAsync()
		{
			await Task.Run(() =>
				schools = DataOperation.ConnectToBackendWithoutParam(SchoolManager.LoadSchools) ??
					new List<School>());
			FilteredSchools = schools;
		}

		//* Private Enumerations
		private enum UIState
		{
			SignUpView,
			SchoolEnrollmentView,
			NewSchoolView
		}
	}
}