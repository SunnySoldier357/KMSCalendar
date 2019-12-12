using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using ModelValidation;

using Xamarin.Forms;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Views;
using KMSCalendar.Services.Data;
using KMSCalendar.Services.Email;

namespace KMSCalendar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        //* Private Properties
        private int logInAttempts;

        private string email;
        private string loginValidationMessage;
        private string password;

        //* Public Properties
        public ICommand AuthenticateUserCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand NewUserCommand { get; set; }

        public int LogInAttempts
        {
            get => logInAttempts;
            set => setProperty(ref logInAttempts, value);
        }

        [ContainsCharacter('@')]
        [DoesNotContainCharacter(' ')]
        [MinimumLength(5)]
        [MaximumLength(254)]
        public string Email
        {
            get => email;
            set => setProperty(ref email, value);
        }
        public string LoginValidationMessage
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                    return Errors[0];

                return loginValidationMessage;
            }
            set => setProperty(ref loginValidationMessage, value);
        }
        [MinimumLength(8)]
        [MaximumLength(64)]
        public string Password
        {
            get => password;
            set => setProperty(ref password, value);
        }

        //* Constructor
        public LoginViewModel()
        {
            Title = "Log In";

            Email = string.Empty;
            Password = string.Empty;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Errors))
                    OnNotifyPropertyChanged(nameof(LoginValidationMessage));
            };

            AuthenticateUserCommand = new Command(async () => await ExecuteAuthenticateUserCommand());
            ForgotPasswordCommand = new Command(async () => await ExecuteForgotPasswordCommand());
            NewUserCommand = new Command(() => (Application.Current as App).MainPage = new SignUpPage());
        }

        //* Public Methods
        public async Task ExecuteAuthenticateUserCommand()
        {
            if (Validate())
            {
                var dataStore = DependencyService.Get<IDataStore<User>>();
                var users = await dataStore.GetItemsAsync(true);
                User signedInUser = users.FirstOrDefault(u => u.Email == Email.Trim());

                if (signedInUser == null)
                    LoginValidationMessage = "This email does not have an account, please sign up for an account";
                else
                {
                    if (PasswordHasher.ValidatePassword(Password, signedInUser.Password))
                    {
                        App app = Application.Current as App;

                        app.SignedInUser = signedInUser;
                        // Sets signedInUser model's SchoolId what it is in the db
                        app.SignedInUser.SchoolId = Services.DataManagers.UserManager.LoadSchoolId(app.SignedInUser.Id);

                        Settings.DefaultInstance.SignedInUserId = signedInUser.Id;

                        app.MainPage = new MainPage();
                    }
                    else
                        LoginValidationMessage = "Invalid Password";
                }
            }
        }

        public async Task ExecuteForgotPasswordCommand()
        {
            if (string.IsNullOrWhiteSpace(Email))
                loginValidationMessage = "Please enter an email first.";
            else
            {
                var emailService = new EmailService();

                var dataStore = DependencyService.Get<IDataStore<User>>();  //does not get user's schoolId
                var users = await dataStore.GetItemsAsync(true);
                User recipient = users.FirstOrDefault(user => user.Email == Email);

                if (recipient == null)
                    LoginValidationMessage = "This email does not have an account, please sign up for an account";
                else
                    emailService.SendResetPasswordEmail(recipient);
            }
        }
    }
}