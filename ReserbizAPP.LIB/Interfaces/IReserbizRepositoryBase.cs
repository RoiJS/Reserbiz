using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizRepositoryBase
    {
        void Add<T>(T entity);
        void Delete<T>(T entity);
        Task<bool> SaveAll();
    }
}