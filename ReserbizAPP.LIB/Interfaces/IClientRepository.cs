using System;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> RegisterClient(Client client);
    }
}