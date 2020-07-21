using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels
{
    public class RegisterViewModel
    {

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Input the birthdate")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Input the user name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Input the first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Input the last name")]
        public string LastName { get; set; }

        [Display(Name = "VAT Number")]
        [Required(ErrorMessage = "Input the VAT number")]
        public long VatNumber { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
