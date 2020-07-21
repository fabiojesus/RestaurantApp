
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords
{
    public class Title : NamedEntity
    {

        #region Properties
        private string _description;
        
        [Column("Description")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RegisterChange();
            }
        }

        private string _position;
        
        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                RegisterChange();
            }
        }
        #endregion

        #region Relationships
        public virtual ICollection<StaffTitle> TitleStaffRecords { get; set; }
        #endregion

        #region Constructor
        
        public Title(string name, string description, string position) : base(name)
        {
            _description = description;
            _position = position;
        }

        public Title(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string description, string position) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _description = description;
            _position = position;
        }
        #endregion


    }
}
