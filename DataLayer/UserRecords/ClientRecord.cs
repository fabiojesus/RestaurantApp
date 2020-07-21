using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.UserRecords
{
    public class ClientRecord : Entity
    {
        #region Properties
        private DateTime _registerDate;
        
        public DateTime RegisterDate
        {
            get => _registerDate;
            set
            {
                _registerDate = value;
                RegisterChange();
            }
        }
        #endregion


        #region Relationships
        private Guid _personId;

        
        [ForeignKey("Person")]
        public Guid PersonId
        {
            get => _personId;
            set
            {
                _personId = value;
                RegisterChange();
            }
        }

        public virtual Person Person { get; set; }

        private Guid _restaurantId;

        
        [ForeignKey("Restaurant")]
        public Guid RestaurantId
        {
            get => _restaurantId;
            set
            {
                _restaurantId = value;
                RegisterChange();
            }
        }
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        #endregion

        #region Constructors
        
        public ClientRecord(Guid personId, Guid restaurantId, DateTime registerDate) : base()
        {
            _personId = personId;
            _restaurantId = restaurantId;
            _registerDate = registerDate;
        }

        public ClientRecord(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid personId, Guid restaurantId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _personId = personId;
            _restaurantId = restaurantId;
        }
        #endregion
    }
}
