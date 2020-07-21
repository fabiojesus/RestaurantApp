
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class DietaryRestriction : NamedEntity
    {
        #region Properties
        #endregion

        #region Relationships
        public ICollection<Dish> Dishes { get; set; }
        #endregion

        #region Constructors 

        public DietaryRestriction(string name) : base(name)
        {
        }

        public DietaryRestriction(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) : base(id, createdAt, updatedAt, isDeleted, name)
        {
        }

        #endregion
    }
}
