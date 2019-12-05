using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITermMiscellaneousRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {

    }
}