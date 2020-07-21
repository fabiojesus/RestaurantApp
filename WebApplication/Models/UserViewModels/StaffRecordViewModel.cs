using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels
{
    public class StaffRecordViewModel : BaseViewModel
    {
        [Display(Name = "Begin Date")]
        [Required(ErrorMessage = "Input the begin date")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Restaurant")]
        [Required(ErrorMessage = "Select a restaurant")]
        public Guid RestaurantId { get; set; }

        [Display(Name = "Profile")]
        [Required(ErrorMessage = "Select a profile")]
        public Guid PersonId { get; set; }

        public StaffRecordViewModel() { }

        public static StaffRecordViewModel Parse(StaffRecord record)
        {
            return new StaffRecordViewModel()
            {
                BeginDate = record.BeginDate,
                EndDate = record.EndDate,
                PersonId = record.PersonId,
                RestaurantId = record.RestaurantId,
            };
        }

        public StaffRecord ToModel()
        {
            return new StaffRecord(PersonId, RestaurantId, BeginDate, EndDate);
        }

        public StaffRecord ToModel(StaffRecord model)
        {
            model.PersonId = PersonId;
            model.RestaurantId = RestaurantId;
            model.BeginDate = BeginDate;
            model.EndDate = EndDate;
            return model;
        }

        public bool CompareToModel(StaffRecord model)
        {
            return BeginDate == model.BeginDate &&
                    EndDate == model.EndDate &&
                    PersonId == model.PersonId &&
                    RestaurantId == model.RestaurantId;
        }
    }
}
