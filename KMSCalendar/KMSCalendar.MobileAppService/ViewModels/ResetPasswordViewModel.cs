using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KMSCalendar.MobileAppService.ViewModels
{
    public class ResetPasswordViewModel : IValidatableObject
    {
        //* Public Properties
        public string AuthToken { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters, and cannot be longer than 64 characters")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters, and cannot be longer than 64 characters")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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