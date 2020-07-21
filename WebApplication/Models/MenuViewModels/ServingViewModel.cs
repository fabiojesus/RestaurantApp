using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class ServingViewModel : BaseViewModel
    {
        [Display(Name = "Menu")]
        [Required(ErrorMessage = "Select a menu")]
        public Guid MenuId { get; set; }

        [Display(Name = "Course")]
        [Required(ErrorMessage = "Select a course")]
        public Guid CourseId { get; set; }

        [Display(Name = "Dish")]
        [Required(ErrorMessage = "Select a dish")]
        public Guid DishId { get; set; }

        public static ServingViewModel Parse(Serving vm)
        {
            return new ServingViewModel()
            {
                MenuId = vm.MenuId,
                CourseId = vm.CourseId,
                DishId = vm.DishId
            };
        }

        public Serving ToModel(Serving model)
        {
            model.MenuId = MenuId;
            model.CourseId = CourseId;
            model.DishId = DishId;
            return model;
        }

        public Serving ToModel()
        {
            return new Serving(MenuId, CourseId, DishId);
        }

        public bool CompareToModel(Serving model)
        {
            return MenuId == model.MenuId && CourseId == model.CourseId && DishId == model.DishId;
        }
    }
}
