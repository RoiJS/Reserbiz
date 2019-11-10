using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContactPersonRepository<TEntity> 
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
         
    }
}