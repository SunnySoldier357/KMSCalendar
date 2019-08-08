using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KMSCalendar.MobileAppService.ViewModels
{
    public class ResetPasswordViewModel : IValidatableObject
    {
        //* Public Properties
        public string AuthToken { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        //* Constructors
        public ResetPasswordViewModel(string authToken) =>
            AuthToken = authToken;

        public ResetPasswordViewModel() { }

        //* Interface Implementations
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO: Authenticate the AuthToken

            if (Password != ConfirmPassword)
                yield return new ValidationResult("The passwords do not match.");
        }
    }
}