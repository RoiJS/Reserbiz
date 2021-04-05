using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IGlobalErrorLogRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task RegisterGlobalError(string source, string message, string stackTrace, int clientId);
    }
}