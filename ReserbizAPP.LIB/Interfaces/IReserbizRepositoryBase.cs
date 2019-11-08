using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizRepositoryBase
    {
        void AddEntity<T>(T entity);
        void DeleteEntity<T>(T entity);
        Task<bool> SaveChangesAsync();
    }
}