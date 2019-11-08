using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizClientRepository : IReserbizRepositoryBase
    {
        ReserbizClientDataContext Context { get; }
    }
}