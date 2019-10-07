using System;

namespace ReserbizAPP.LIB.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime DateDeleted { get; set; }
        public DateTime DateDeactivated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}