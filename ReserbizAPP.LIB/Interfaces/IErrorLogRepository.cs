using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IErrorLogRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task RegisterError(string source, string message, string stackTrace, string userInfo);
    }
}