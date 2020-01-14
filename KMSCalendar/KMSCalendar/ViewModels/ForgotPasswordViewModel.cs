using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using Autofac;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Services.Email;
using Microsoft.AppCenter.Crashes;
using ModelValidation;

using PropertyChanged;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class ForgotPasswordViewModel : BaseViewModel
	{
		//* Constants

		/// <summary>
		/// Determines the length of the token sent to the user. Has
		/// to be between 1 & 32.
		/// </summary>
		public const int TOKEN_SIZE = 6;

		//* Private Properties
		private readonly IEmailService emailService;

		private string token;
		private string validationMessage;

		private UIState currentUIState { get; set; } = UIState.EmailView;

		//* Public Properties
		public bool EmailVisibility => currentUIState == UIState.EmailView;
		public bool GoBackVisibility => EmailVisibility || VerificationVisibility;
		public bool NewPasswordVisibility => currentUIState == UIState.NewPasswordView;
		public bool SuccessVisibility => currentUIState == UIState.SuccessView;
		public bool VerificationVisibility => currentUIState == UIState.CodeView;

		public ICommand AuthenticateCodeCommand { get; }
		public ICommand AuthenticateEmailCommand { get; }
		public ICommand AuthenticateNewPasswordCommand { get; }
		public ICommand ExitCommand { get; }
		public ICommand GoBackCommand { get; }

		public string Code { get; set; }
		[PropertyValueMatch(nameof(Password),
			ErrorMessage = "The Passwords do not match!")]
		public string ConfirmPassword { get; set; }
		[ContainsCharacter('@')]
		[DoesNotContainCharacter(' ')]
		[MinimumLength(5)]
		[MaximumLength(254)]
		public string Email { get; set; }
		[MinimumLength(8)]
		[MaximumLength(64)]
		public string Password { get; set; }
		public string ValidationMessage
		{
			get
			{
				if (Errors != null && Errors.Count > 0)
					return Errors[0];

				return validationMessage;
			}
			set => setProperty(ref validationMessage, value);
		}

		//* Constructors
		public ForgotPasswordViewModel() :
			this(AppContainer.Container.Resolve<IEmailService>()) { }

		public ForgotPasswordViewModel(IEmailService emailService)
		{
			this.emailService = emailService;

			token = Guid.NewGuid().ToString().Replace("-", "")
				.Substring(0, TOKEN_SIZE).ToUpper();

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(Errors))
					OnNotifyPropertyChanged(nameof(ValidationMessage));
			};

			AuthenticateCodeCommand = new Command(() => authenticateCode());
			AuthenticateEmailCommand = new Command(async () => await authenticateEmailAsync());
			AuthenticateNewPasswordCommand = new Command(() => authenticateNewPassword());
			ExitCommand = new Command(async () =>
				await App.MainPage.Navigation.PopModalAsync());
			GoBackCommand = new Command(() => goBack());
		}

		//* Private Methods
		private void authenticateCode()
		{
			ValidationMessage = "";

			if (Code?.ToUpper().Equals(token) ?? false)
				currentUIState++;
			else
				ValidationMessage = "Code invalid!";
		}

		private async Task authenticateEmailAsync()
		{
			if (Validate() && Email != null)
			{
				IsBusy = true;
				ValidationMessage = "";

				await Task.Run(() =>
				{
					User user = DataOperation.ConnectToBackend(UserManager.LoadUserFromEmail, Email);
					if (user != null)
					{
						try
						{
							emailService.SendResetPasswordEmail(user, token);
							currentUIState++;
						}
						catch (Exception e)
						{
							Crashes.TrackError(e, new Dictionary<string, string>
							{
								{ "Source", nameof(ForgotPasswordViewModel) }
							});

							ValidationMessage = "Email failed to send";
						}
					}
					else
						ValidationMessage = "Please enter the email address for your account.";
				});

				IsBusy = false;
			}
			else
				ValidationMessage = "Please enter a valid email address.";
		}

		private void authenticateNewPassword()
		{
			if (Validate() && Password != null)
			{
				ValidationMessage = "";

				User user = DataOperation.ConnectToBackend(UserManager.LoadUserFromEmail, Email);

				if (user != null)
				{
					user.Password = PasswordHasher.HashPassword(Password);

					// Resets the user's password in the DB
					DataOperation.ConnectToBackend(UserManager.UpdateUser, user);

					currentUIState++;
				}
			}
			else
				ValidationMessage = "Please enter a password.";
		}

		private void goBack()
		{
			if (--currentUIState < 0)
				App.MainPage.Navigation.PopModalAsync();
		}

		//* Private Enumerations
		private enum UIState
		{
			EmailView,
			CodeView,
			NewPasswordView,
			SuccessView
		}
	}
}