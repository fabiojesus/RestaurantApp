
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.DataLayer.Base
{
    public abstract class NamedEntity : Entity
    {
        private string _name;

        
        [Required(ErrorMessage ="Input name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RegisterChange();
            }
        }

        protected NamedEntity(string name)
        {
            _name = name;
        }

        protected NamedEntity(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) : base(id, createdAt, updatedAt, isDeleted)
        {
            _name = name;
        }
    }
}
