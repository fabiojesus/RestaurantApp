
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class Menu : DatedEntity
    {
        #region Relationships
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

        private Guid _mealId;

        
        [ForeignKey("Meal")]
        public Guid MealId
        {
            get => _mealId;
            set
            {
                _mealId = value;
                RegisterChange();
            }
        }

        public virtual Meal Meal { get; set; }

        public virtual ICollection<Serving> Servings { get; set; }
        #endregion

        #region Constructors

        public Menu(DateTime date, Guid restaurantId, Guid mealId) : base(date)
        {
            _restaurantId = restaurantId;
            _mealId = mealId;
        }

        public Menu(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, DateTime date, Guid restaurantId, Guid mealId) : base(id, createdAt, updatedAt, isDeleted, date)
        {
            _restaurantId = restaurantId;
            _mealId = mealId;
        }
        #endregion

    }
}
