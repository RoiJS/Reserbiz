namespace ReserbizAPP.LIB.Interfaces
{
    public interface ISpaceTypeRepository<TEntity> 
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        
    }
}