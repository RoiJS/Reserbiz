using System;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> RegisterClient(Client client);

        Task<bool> ClientExists(string customerName, string dbName);

        Task<Client> GetCompanyInfoByName(string companyName);
    }
}