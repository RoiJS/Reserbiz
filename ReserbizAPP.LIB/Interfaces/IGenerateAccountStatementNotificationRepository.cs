namespace ReserbizAPP.LIB.Interfaces
{
    public interface IGenerateAccountStatementNotificationRepository<TEntity>
         : IBaseNotificationRepository, IBaseRepository<TEntity> where TEntity : class, IEntity
    {
         
    }
}