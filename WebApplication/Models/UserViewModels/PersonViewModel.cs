using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.UserViewModels
{
    public class PersonViewModel : BaseViewModel
    {

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Input the birthdate")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Input the first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Input the last name")]
        public string LastName { get; set; }

        [Display(Name = "VAT Number")]
        [Required(ErrorMessage = "Input the VAT number")]
        public long VatNumber { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Input the phone number")]
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        public PersonViewModel() { }

        public static PersonViewModel Parse(Person record)
        {
            return new PersonViewModel()
            {
                BirthDate = record.BirthDate,
                FirstName = record.FirstName,
                LastName = record.LastName,
                VatNumber = record.VatNumber,
                PhoneNumber = record.PhoneNumber,
            };
        }

        public Person ToModel()
        {
            return new Person(BirthDate, FirstName, LastName,VatNumber, PhoneNumber);
        }

        public Person ToModel(Person model)
        {
            model.BirthDate = BirthDate;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.VatNumber = VatNumber;
            model.PhoneNumber = PhoneNumber;
            return model;
        }

        public bool CompareToModel(Person model)
        {
            return BirthDate == model.BirthDate &&
                    FirstName == model.FirstName &&
                    LastName == model.LastName &&
                    VatNumber == model.VatNumber &&
                    PhoneNumber == model.PhoneNumber;
        }


    }
}
