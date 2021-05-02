using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITestRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task SendPushNotification();
    }
}