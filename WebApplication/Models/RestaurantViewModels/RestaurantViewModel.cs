using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels
{
    public class RestaurantViewModel : NamedViewModel
    {

        [Required(ErrorMessage = "Input the address")]
        public string Address { get; set; }

        [Display(Name = "Restaurant")]
        [Required(ErrorMessage = "Input the opening hours")]
        public string OpeningHours { get; set; }

        [Display(Name = "Restaurant")]
        [Required(ErrorMessage = "Input the closing hours")]
        public string ClosingHours { get; set; }

        [Display(Name = "Closing days")]
        [Required(ErrorMessage = "Input the vacancy days")]
        public string ClosingDays { get; set; }

        [Display(Name = "Table Count")]
        [Required(ErrorMessage = "Input a number of tables")]
        [Range(0, int.MaxValue)]
        public int TableCount { get; set; }


        public static RestaurantViewModel Parse(Restaurant restaurant)
        {
            return new RestaurantViewModel()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                OpeningHours = restaurant.OpeningHours,
                ClosingHours = restaurant.ClosingHours,
                ClosingDays = restaurant.ClosingDays,
                TableCount = restaurant.TableCount
            };
        }

        public Restaurant ToModel()
        {
            return new Restaurant(Name, Address, OpeningHours, ClosingHours, ClosingDays, TableCount);
        }
        public Restaurant ToModel(Restaurant model)
        {
            model.Name = Name;
            model.Address = Address;
            model.OpeningHours = OpeningHours;
            model.ClosingHours = ClosingHours;
            model.ClosingDays = ClosingDays;
            model.TableCount = TableCount;
            return model;
        }


        public bool CompareToModel(Restaurant model)
        {
            return Name == model.Name &&
                    Address == model.Address &&
                    OpeningHours == model.OpeningHours &&
                    ClosingHours == model.ClosingHours &&
                    ClosingDays == model.ClosingDays &&
                    TableCount == model.TableCount;
        }
    }
}
