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