using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class Course : NamedEntity
    {
        #region Relationships
        public ICollection<Serving> Servings { get; set; }
        #endregion

        #region Constructors 
        public Course(string name) : base(name)
        {
        }

        public Course(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) : base(id, createdAt, updatedAt, isDeleted, name)
        {
        }

        #endregion
    }
}
