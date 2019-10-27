using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IDataContextHelper
    {
         void GenerateEntityUpdateDate(List<EntityEntry> entries);
         void GenerateEntityCreatedDate(List<EntityEntry> entries);
    }
}