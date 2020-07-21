using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.Base
{
    public class DatedViewModel : BaseViewModel
    {
        [Required(ErrorMessage ="Insert the date")]
        public virtual DateTime Date { get; set; }
    }
}
