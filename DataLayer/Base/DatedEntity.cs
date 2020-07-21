
using System;

namespace Recodme.Academy.RestaurantApp.DataLayer.Base
{
    public abstract class DatedEntity : Entity
    {
        private DateTime _date;

        
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                RegisterChange();
            }
        }

        protected DatedEntity(DateTime date)
        {
            _date = date;
        }

        protected DatedEntity(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, DateTime date) : base(id, createdAt, updatedAt, isDeleted)
        {
            _date = date;
        }
    }
}
