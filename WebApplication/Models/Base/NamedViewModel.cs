using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.Base
{
    public class NamedViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Insert the name")]
        public string Name { get; set; }

    }
}
