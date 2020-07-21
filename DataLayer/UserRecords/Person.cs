
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recodme.Academy.RestaurantApp.DataLayer.UserRecords
{
    public class Person : Entity
    {
        #region Properties
        private long _vatNumber;
        
        public long VatNumber
        {
            get => _vatNumber;
            set
            {
                _vatNumber = value;
                RegisterChange();
            }
        }

        private string _firstName;
        
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RegisterChange();
            }
        }

        private string _lastName;
        
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RegisterChange();
            }
        }

        private DateTime _birthDate;

        
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RegisterChange();
            }
        }

        private long _phoneNumber;
        [DataType(DataType.PhoneNumber)]
        
        public long PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RegisterChange();
            }
        }
        #endregion

        #region Relationships

        public virtual ICollection<StaffRecord> StaffRecords { get; set; }

        public virtual ICollection<ClientRecord> ClientRecords { get; set; }

        #endregion

        #region Constructor
        
        public Person(DateTime birthDate, string firstName, string lastName, long vatNumber, long phoneNumber) : base()
        {
            _birthDate = birthDate;
            _firstName = firstName;
            _lastName = lastName;
            _vatNumber = vatNumber;
            _phoneNumber = phoneNumber;
        }

        public Person(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, DateTime birthDate, string firstName, string lastName, long vatNumber, long phoneNumber) : base(id, createdAt, updatedAt, isDeleted)
        {
            _birthDate = birthDate;
            _firstName = firstName;
            _lastName = lastName;
            _vatNumber = vatNumber;
            _phoneNumber = phoneNumber;
        }
        #endregion

    }
}
