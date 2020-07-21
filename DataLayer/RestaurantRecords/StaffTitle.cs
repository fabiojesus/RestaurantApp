
using Recodme.Academy.RestaurantApp.DataLayer.Base;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords
{
    public class StaffTitle : Entity
    {
        #region Properties

        private DateTime _beginDate;

        
        public DateTime BeginDate
        {
            get => _beginDate;
            set
            {
                _beginDate = value;
                RegisterChange();
            }
        }

        private DateTime _endDate;

        
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                RegisterChange();
            }
        }

        #endregion

        #region Relationships

        private Guid _staffId;


        [ForeignKey("Staff")]
        
        public Guid StaffId
        {
            get => _staffId;
            set
            {
                _staffId = value;
                RegisterChange();
            }
        }

        public virtual StaffRecord Staff { get; set; }

        private Guid _titleId;

        
        [ForeignKey("Title")]
        public Guid TitleId
        {
            get => _titleId;
            set
            {
                _titleId = value;
                RegisterChange();
            }
        }

        public virtual Title Title { get; set; }

        #endregion

        #region Constructor
        
        public StaffTitle(DateTime beginDate, DateTime endDate, Guid staffId, Guid titleId) : base()
        {
            _beginDate = beginDate;
            _endDate = endDate;
            _staffId = staffId;
            _titleId = titleId;
        }

        public StaffTitle(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted,
                          DateTime beginDate, DateTime endDate, Guid staffId, Guid titleId) :
                            base(id, createdAt, updatedAt, isDeleted)
        {
            _beginDate = beginDate;
            _endDate = endDate;
            _staffId = staffId;
            _titleId = titleId;
        }
        #endregion

    }
}
