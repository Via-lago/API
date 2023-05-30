using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public int OTP { get; set; }
        
        [PasswordValidation]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
