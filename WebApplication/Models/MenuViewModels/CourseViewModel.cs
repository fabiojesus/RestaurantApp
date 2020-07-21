using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.MenuViewModels
{
    public class CourseViewModel : NamedViewModel
    {
        public CourseViewModel() { }

        public static CourseViewModel Parse(Course course)
        {
            var cvm = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name
            };
            return cvm;
        }

        public Course ToModel()
        {
            return new Course(Name);
        }

        public Course ToModel(Course model)
        {
            model.Name = Name;
            return model;
        }

        public bool CompareToModel(Course model)
        {
            return Name == model.Name;
        }
    }
}
