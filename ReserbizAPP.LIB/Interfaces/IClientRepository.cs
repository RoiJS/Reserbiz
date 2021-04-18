using System;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IClientRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<Client> RegisterClient(Client client);
        Task<Client> RegisterDemo(Client client);
        Task<Client> GetCompanyInfoByName(string companyName);
        Task CreateClientDatabase(Client client);
        Task PopulateDatabase(UserAccount userAccount, Client client, Action<UserAccount> sendEmaiNotification);
        void SendNewClientRegisteredEmailNotification(UserAccount userAccount, Client client);
        void SendNewDemoRegisteredEmailNotification(UserAccount userAccount, Client client);
    }
}