using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizSystemRepository : IReserbizRepositoryBase
    {
        ReserbizDataContext Context { get; }
    }
}