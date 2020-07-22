using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Input your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Input your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
