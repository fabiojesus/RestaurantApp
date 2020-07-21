
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords
{
    public class Booking : DatedEntity
    {

        #region Relationships

        private Guid _clientId;

        
        [ForeignKey("Client")]
        public Guid ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                RegisterChange();
            }
        }

        public virtual ClientRecord Client { get; set; }


        #endregion

        #region Constructor

        
        public Booking(DateTime date, Guid clientId) : base(date)
        {
            _clientId = clientId;
        }

        public Booking(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, DateTime date, Guid clientId) : base(id, createdAt, updatedAt, isDeleted, date)
        {
            _clientId = clientId;
        }

        #endregion

    }
}
