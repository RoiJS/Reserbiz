namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPenaltyBreakdownRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        
    }
}