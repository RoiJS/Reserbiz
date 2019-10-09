using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class SpaceType : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public int AvailableSlot { get; set; }

    }
}