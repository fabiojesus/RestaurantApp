using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels
{
    public class StaffTitleViewModel : BaseViewModel
    {
        [Display(Name = "Begin Date")]
        [Required(ErrorMessage = "Input the begin date")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Staff Member")]
        [Required(ErrorMessage = "Select a staff member")]
        public Guid StaffId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Select a title")] 
        public Guid TitleId { get; set; }

        public StaffTitleViewModel() { }


        public static StaffTitleViewModel Parse(StaffTitle restaurant)
        {
            return new StaffTitleViewModel()
            {
                BeginDate = restaurant.BeginDate,
                EndDate = restaurant.EndDate,
                StaffId = restaurant.StaffId,
                TitleId = restaurant.TitleId,
            };
        }

        public StaffTitle ToModel()
        {
            return new StaffTitle(BeginDate, EndDate, StaffId, TitleId);
        }

        public StaffTitle ToModel(StaffTitle model)
        {
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            model.StaffId = StaffId;
            model.TitleId = TitleId;
            return model;
        }

        public bool CompareToModel(StaffTitle model)
        {
            return BeginDate == model.BeginDate &&
                    EndDate == model.EndDate &&
                    StaffId == model.StaffId &&
                    TitleId == model.TitleId;
        }
    }
}
