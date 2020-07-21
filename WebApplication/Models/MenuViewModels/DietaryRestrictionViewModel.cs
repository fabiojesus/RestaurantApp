using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class DietaryRestrictionViewModel : NamedViewModel
    {

        public DietaryRestrictionViewModel() { }


        public DietaryRestriction ToModel()
        {
            return new DietaryRestriction(Name); 
        }

        public DietaryRestriction ToModel(DietaryRestriction model)
        {
            model.Name = Name;
            return model;
        }

        public static DietaryRestrictionViewModel Parse(DietaryRestriction model)
        {
            return new DietaryRestrictionViewModel() { Name = model.Name, Id = model.Id };
        }

        public bool CompareToModel(DietaryRestriction model)
        {
            return Name == model.Name;
        }
    }
}
