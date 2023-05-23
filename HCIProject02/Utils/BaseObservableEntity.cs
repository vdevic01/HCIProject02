using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Utils
{
    internal class BaseObservableEntity : ObservableEntity, IBaseEntity
    {
        #region Properties
        private Guid _id;
        public Guid Id { get => _id; set => OnPropertyChanged(ref _id, value); }

        private DateTime _createdAt = DateTime.Now;
        public DateTime CreatedAt { get => _createdAt; set => OnPropertyChanged(ref _createdAt, value); }

        private bool _isActive = true;
        public bool IsActive { get => _isActive; set => OnPropertyChanged(ref _isActive, value); }
        #endregion

        #region Constructors
        public BaseObservableEntity() { }

        public BaseObservableEntity(BaseObservableEntity other)
        {
            _id = other.Id;
            _createdAt = other._createdAt;
            _isActive = other.IsActive;
        }

        #endregion
    }
}
