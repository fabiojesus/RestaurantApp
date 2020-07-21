
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class Dish : NamedEntity
    {

        #region Properties
        #endregion

        #region Relationships
        public ICollection<Serving> Servings { get; set; }

        public virtual DietaryRestriction DietaryRestriction { get; set; }

        private Guid _dietaryRestrictionId;

        
        [ForeignKey("DietaryRestriction")]
        [Display(Name = "Dietary Restriction")]
        public Guid DietaryRestrictionId
        {
            get => _dietaryRestrictionId;
            set
            {
                _dietaryRestrictionId = value;
                RegisterChange();
            }
        }
        #endregion

        #region Constructors 
        
        public Dish(string name, Guid dietaryRestrictionId) : base(name)
        {
            _dietaryRestrictionId = dietaryRestrictionId;
        }

        public Dish(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, Guid dietaryRestrictionId) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _dietaryRestrictionId = dietaryRestrictionId;
        }

        #endregion
    }
}
