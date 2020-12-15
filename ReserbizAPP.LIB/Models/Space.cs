
using System.Collections.Generic;
using System.Linq;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class Space
        : Entity, IUserActionTracker
    {

        public string Description { get; set; }
        public int SpaceTypeId { get; set; }
        public SpaceType SpaceType { get; set; }

        public List<Contract> Contracts { get; set; }

        public bool IsDeletable
        {

            get
            {
                // Being not occupied also means
                // that space is deletable
                return IsNotOccupied;
            }
        }

        public bool IsNotOccupied
        {
            get
            {
                // We can determine if space is available 
                // if it is not used, or in other words,
                // it is not attached to any ACTIVE contract yet
                return (ActiveContracts.Count == 0);
            }
        }

        public int OccupiedByContractId
        {
            // If the ACTIVE contract where space is attached to is not yet archived, 
            // we return the correct contract id, otherwise we return 0 indicating
            // that the space is not currently occupied.
            get
            {
                return ActiveContracts.Count > 0 ? ActiveContracts[0].Id : 0;
            }
        }

        public Space()
        {
            Contracts = new List<Contract>();
        }

        public int? DeletedById { get; set; }
        public Account DeletedBy { get; set; }
        public int? UpdatedById { get; set; }
        public Account UpdatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public int? DeactivatedById { get; set; }
        public Account DeactivatedBy { get; set; }

        private List<Contract> ActiveContracts 
        {
            get {
                return Contracts.Where(c => c.IsArchived == false).ToList();
            }
        }
    }
}
