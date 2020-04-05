using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IDataContextHelper
    {
        void GenerateEntityUpdateDateAndUpdatedById(List<EntityEntry> entries, int? currentUserId = null);
        void GenerateEntityCreatedDateAndCreatedById(List<EntityEntry> entries, int? currentUserId = null);
    }
}