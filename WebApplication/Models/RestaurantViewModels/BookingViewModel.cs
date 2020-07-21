using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using Recodme.Academy.RestaurantApp.WebApplication.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Academy.RestaurantApp.WebApplication.Models.RestaurantViewModels
{
    public class BookingViewModel : DatedViewModel
    {
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Select a client")]
        public Guid ClientId { get; set; }
        public BookingViewModel() { }

        public static BookingViewModel Parse(Booking booking)
        {
            return new BookingViewModel()
            {
                ClientId = booking.ClientId,
                Date = booking.Date
            };
        }

        public Booking ToModel()
        {
            return new Booking(Date, ClientId);
        }


        public Booking ToModel(Booking model)
        {
            model.Date = Date;
            model.ClientId = ClientId;
            return model;
        }

        public bool CompareToModel(Booking model)
        {
            return Date == model.Date && ClientId == model.ClientId;
        }
    }
}
