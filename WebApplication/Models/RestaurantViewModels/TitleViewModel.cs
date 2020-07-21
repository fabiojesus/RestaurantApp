using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels
{
    public class TitleViewModel : NamedViewModel
    {
        [Required(ErrorMessage = "Input the description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Input the position")]
        public string Position { get; set; }

        public static TitleViewModel Parse(Title title)
        {
            return new TitleViewModel()
            {
                Name = title.Name,
                Description = title.Description,
                Position = title.Position,
            };
        }

        public Title ToModel()
        {
            return new Title(Name, Description, Position);
        }

        public Title ToModel(Title model)
        {
            model.Name = Name;
            model.Position = Position;
            model.Description = Description;
            return model;
        }

        public bool CompareToModel(Title model)
        {
            return Name == model.Name &&
                    Description == model.Description &&
                    Position == model.Position;
        }
    }
}
