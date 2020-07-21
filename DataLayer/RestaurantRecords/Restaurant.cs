
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords
{
    public class Restaurant : NamedEntity
    {
        #region Properties

        private string _address;

        
        [Required(ErrorMessage = "Input the address")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RegisterChange();
            }
        }

        private string _openingHours;
        
        public string OpeningHours
        {
            get => _openingHours;
            set
            {
                _openingHours = value;
                RegisterChange();
            }
        }

        private string _closingDays;
        
        public string ClosingDays
        {
            get => _closingDays;
            set
            {
                _closingDays = value;
                RegisterChange();
            }
        }

        private string _closingHours;
        
        public string ClosingHours
        {
            get => _closingHours;
            set
            {
                _closingHours = value;
                RegisterChange();
            }
        }

        private int _tableCount;

        [Range(1, 100)]
        
        public int TableCount
        {
            get => _tableCount;
            set
            {
                _tableCount = value;
                RegisterChange();
            }
        }
        #endregion

        #region Relationships
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<ClientRecord> Clients { get; set; }
        public virtual ICollection<StaffRecord> StaffMembers { get; set; }
        #endregion

        #region Constructor

        
        public Restaurant(string name, string address, string openingHours, string closingHours, string closingDays, int tableCount) : base(name)
        {
            _openingHours = openingHours;
            _closingDays = closingDays;
            _closingHours = closingHours;
            _address = address;
            _tableCount = tableCount;
        }

        public Restaurant(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string address, string openingHours, string closingHours, string closingDays) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _openingHours = openingHours;
            _closingDays = closingDays;
            _closingHours = closingHours;
            _address = address;
        }


        #endregion


    }
}
