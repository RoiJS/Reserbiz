namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentRegisterNotificationRepository<TEntity>
         : IBaseNotificationRepository, IBaseRepository<TEntity> where TEntity : class, IEntity
    {
         
    }
}