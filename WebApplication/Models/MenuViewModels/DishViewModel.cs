using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class DishViewModel : NamedViewModel
    {
        [Display(Name ="Dietary Restriction")]
        [Required(ErrorMessage = "Select a dietary restriction")]
        public Guid DietaryRestrictionId { get; set; }
        public DishViewModel() { }
       

        public static DishViewModel Parse(Dish d)
        {
            return new DishViewModel() { Name = d.Name, Id = d.Id, DietaryRestrictionId=d.DietaryRestrictionId };
        }
        public Dish ToModel()
        {
            return new Dish(Name, DietaryRestrictionId);
        }

        public Dish ToModel(Dish model)
        {
            model.Name = Name;
            model.DietaryRestrictionId = DietaryRestrictionId;
            return model;
        }

        public bool CompareToModel(Dish model)
        {
            return Name == model.Name && DietaryRestrictionId == model.DietaryRestrictionId;
        }
    }
}
