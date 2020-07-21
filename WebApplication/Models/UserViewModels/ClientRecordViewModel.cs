using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels
{
    public class ClientRecordViewModel : BaseViewModel
    {
        [Display(Name = "Profile")]
        [Required(ErrorMessage = "Select a profile")]
        public Guid PersonId { get; set; }

        [Display(Name = "Restaurant")]
        [Required(ErrorMessage = "Select a restaurant")] 
        public Guid RestaurantId { get; set; }

        [Display(Name = "Register Date")]
        [Required(ErrorMessage = "Input the register date")]
        public DateTime RegisterDate { get; set; }

        public static ClientRecordViewModel Parse(ClientRecord record)
        {
            return new ClientRecordViewModel()
            {
                PersonId = record.PersonId,
                RestaurantId = record.RestaurantId,
                RegisterDate = record.RegisterDate,
            };
        }

        public ClientRecord ToModel()
        {
            return new ClientRecord(PersonId, RestaurantId, RegisterDate);
        }

        public ClientRecord ToModel(ClientRecord model)
        {
            model.PersonId = PersonId;
            model.RestaurantId = RestaurantId;
            model.RegisterDate = RegisterDate;
            return model;
        }

        public bool CompareToModel(ClientRecord model)
        {
            return PersonId == model.PersonId &&
                    RestaurantId == model.RestaurantId &&
                    RegisterDate == model.RegisterDate;
        }
    }
}
