using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class Meal : NamedEntity
    {

        #region Properties
        public string StartingHours { get; set; }
        public string EndingHours { get; set; }
        #endregion

        #region Relationships
        public ICollection<Menu> Menus { get; set; }
        #endregion

        #region Constructors 
        public Meal(string name, string startingHours, string endingHours) : base(name)
        {
            StartingHours = startingHours;
            EndingHours = endingHours;
        }

        public Meal(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) : base(id, createdAt, updatedAt, isDeleted, name)
        {
        }
        #endregion
    }
}
