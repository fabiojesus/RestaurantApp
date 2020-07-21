using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class MealViewModel : NamedViewModel
    {
        [Display(Name="Starting Hours")]
        [Required(ErrorMessage = "Insert the starting hours")]
        public string StartingHours { get; set; }

        [Display(Name = "Ending Hours")]
        [Required(ErrorMessage = "Insert the ending hours")]
        public string EndingHours { get; set; }

        public MealViewModel() { }

        public static MealViewModel Parse(Meal meal)
        {
            return new MealViewModel() { Id = meal.Id, Name = meal.Name, StartingHours = meal.StartingHours, EndingHours = meal.EndingHours };
        }

        public Meal ToModel()
        {
            return new Meal(Name, StartingHours, EndingHours);
        }

        public Meal ToModel(Meal model)
        {
            model.Name = Name;
            model.StartingHours = StartingHours;
            model.EndingHours = EndingHours;
            return model;
        }

        public bool CompareToModel(Meal model)
        {
            return Name == model.Name && StartingHours == model.StartingHours && EndingHours == model.EndingHours;
        }
    }
}
