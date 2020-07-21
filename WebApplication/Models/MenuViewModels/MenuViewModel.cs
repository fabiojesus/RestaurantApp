using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class MenuViewModel : DatedViewModel
    {
        [Display(Name = "Restaurant")]
        [Required(ErrorMessage = "Select a restaurant")]
        public Guid RestaurantId { get; set; }
        
        [Display(Name = "Meal")]
        [Required(ErrorMessage = "Select a meal")] 
        public Guid MealId { get; set; }

        public static MenuViewModel Parse(Menu vm)
        {
            return new MenuViewModel()
            {
                Id = vm.Id,
                Date = vm.Date,
                RestaurantId = vm.RestaurantId,
                MealId = vm.MealId
            };
        }

        public Menu ToModel()
        {
            return new Menu(Date, RestaurantId, MealId);
        }


        public Menu ToModel(Menu model)
        {
            model.RestaurantId = RestaurantId;
            model.MealId = MealId;
            model.Date = Date;
            return model;
        }

        public bool CompareToModel(Menu model)
        {
            return Date == model.Date && RestaurantId == model.RestaurantId && MealId == model.MealId;
        }
    }
}
