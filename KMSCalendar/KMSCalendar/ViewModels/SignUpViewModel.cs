using ModelValidation;
using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Views;
using System.Threading.Tasks;
using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services;

namespace KMSCalendar.ViewModels
{
    public class SignUpViewModel : LogInViewModel
    {
        //* Private Properties
        private string confirmPassword;
        private string userName;

        //* Public Properties
        public ICommand AlreadyUserCommand { get; set; }
        public new ICommand AuthenticateUserCommand { get; set; }

        [PropertyValueMatch(nameof(Password))]
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

        //* Constructors
        public SignUpViewModel() : base()
        {
            Title = "Sign Up";

            ConfirmPassword = string.Empty;
            UserName = string.Empty;

            AlreadyUserCommand = new Command(() => ExecuteAlreadyUserCommand());
            AuthenticateUserCommand = new Command(async () => await ExecuteAuthenticateUserCommand());
        }

        //* Public Methods
        public void ExecuteAlreadyUserCommand() =>
            (Application.Current as App).MainPage = new LoginPage();

        public new async Task ExecuteAuthenticateUserCommand()
        {
            if (Validate())
            {
                string hashedPassword = PasswordHasher.HashPassword(Password);

                User user = new User
                {
                    Email = Email,
                    UserName = UserName,
                    Password = hashedPassword
                };

                var dataStore = DependencyService.Get<IDataStore<User>>();
                User signedInUser = await dataStore.AddItemAsync(user);

                App app = Application.Current as App;

                app.SignedInUser = signedInUser;
                Settings.DefaultInstance.SignedInUserId = signedInUser.Id;

                app.MainPage = new MainPage();
            }
        }
    }
}