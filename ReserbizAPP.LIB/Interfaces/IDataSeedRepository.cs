
namespace ReserbizAPP.LIB.Interfaces
{
    public interface IDataSeedRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        void SeedData();
    }
}