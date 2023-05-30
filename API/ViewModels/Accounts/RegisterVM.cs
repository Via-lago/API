using API.Models;
using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts
{
    public class RegisterVM
    {
        public string? NIK { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }
        
        [EmailAddress]
        [EmailPhoneValidation(nameof(Email))]
        public string Email { get; set; }

        [Phone]
        [EmailPhoneValidation(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }


        [Range(0,4,ErrorMessage ="Not valid")]
        public float GPA { get; set; }

        //public Guid UniversityGuid { get; set; }

        [Required]
        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        [PasswordValidation]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        // public University? University { get; set; }


    }
}