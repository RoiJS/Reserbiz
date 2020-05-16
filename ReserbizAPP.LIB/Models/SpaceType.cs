using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class SpaceType
        : Entity, IUserActionTracker
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public int AvailableSlot { get; set; }

        public Term Term { get; set; }

        public bool IsDeletable
        {
            get
            {
                // We can determine if space type can be 
                // removed if it is not used, or in other words,
                // it is not attached to any term yet
                return (Term == null);
            }
        }

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }
    }
}