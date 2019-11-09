using System;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        bool IsDelete { get; set; }
        DateTime DateDeleted { get; set; }
        DateTime DateDeactivated { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}