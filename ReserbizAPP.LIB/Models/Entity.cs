using System;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }
        public DateTime DateDeactivated { get; set; }

        // TODO: Introduced following new class properties
        // to tracked down which user is responsible for 
        // creating, updating, deleting and deactivating an entity
        // public int DeletedById { get; set; }
        // public Account DeletedBy { get; set; }
        // public int UpdatedById { get; set; }
        // public Account UpdatedBy { get; set; }
        // public int CreatedById { get; set; }
        // public Account CreatedBy { get; set; }
        // public int DeactivatedById { get; set; }
        // public Account DeactivatedBy { get; set; }
    }
}