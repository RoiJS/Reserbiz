namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentBreakdownRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        
    }
}