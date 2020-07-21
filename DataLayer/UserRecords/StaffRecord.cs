
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.UserRecords
{
    public class StaffRecord : Entity
    {
        #region Properties
        private DateTime _beginDate;
        
        public DateTime BeginDate
        {
            get => _beginDate;
            set
            {
                _beginDate = value;
                RegisterChange();
            }
        }

        private DateTime _endDate;
        
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
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

        public virtual ICollection<StaffTitle> Titles { get; set; }

        #endregion

        #region Constructor
        
        public StaffRecord(Guid personId, Guid restaurantId, DateTime beginDate, DateTime endDate) : base()
        {
            _personId = personId;
            _restaurantId = restaurantId;
            _beginDate = beginDate;
            _endDate = endDate;
        }

        public StaffRecord(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid personId, Guid restaurantId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _personId = personId;
            _restaurantId = restaurantId;
        }
        #endregion

    }
}
