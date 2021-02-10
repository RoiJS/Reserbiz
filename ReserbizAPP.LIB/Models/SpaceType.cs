using System.Collections.Generic;
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

        public List<Term> Terms { get; set; }
        public List<Space> Spaces { get; set; }

        public bool IsDeletable
        {
            get
            {
                // We can determine if space type can be 
                // removed if it is not used, or in other words,
                // it is not attached to any term or
                // not even used on space  yet.
                return (Terms.Count == 0 && Spaces.Count == 0);
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