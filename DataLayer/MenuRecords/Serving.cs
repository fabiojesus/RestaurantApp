
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.MenuRecords
{
    public class Serving : Entity
    {

        #region Properties
        #endregion

        #region Relationships

        public virtual Course Course { get; set; }

        private Guid _courseId;

        
        [ForeignKey("Course")]
        public Guid CourseId
        {
            get => _courseId;
            set
            {
                _courseId = value;
                RegisterChange();
            }
        }

        public virtual Dish Dish { get; set; }

        private Guid _dishId;

        
        [ForeignKey("Dish")]
        public Guid DishId
        {
            get => _dishId;
            set
            {
                _dishId = value;
                RegisterChange();
            }
        }
        public virtual Menu Menu { get; set; }

        private Guid _menuId;

        
        [ForeignKey("Menu")]
        public Guid MenuId
        {
            get => _menuId;
            set
            {
                _menuId = value;
                RegisterChange();
            }
        }

        #endregion

        #region Constructors 
        
        public Serving(Guid menuId, Guid dishId, Guid courseId)
        {
            _menuId = menuId;
            _dishId = dishId;
            _courseId = courseId;
        }

        public Serving(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid menuId, Guid dishId, Guid courseId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _menuId = menuId;
            _dishId = dishId;
            _courseId = courseId;
        }

        #endregion
    }
}
