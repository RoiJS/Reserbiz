using System;
using System.Threading.Tasks;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ClientRepository : IClientRepository
    {
        private readonly IReserbizRepository _reserbizRepository;
        public ClientRepository(IReserbizRepository reserbizRepository)
        {
            _reserbizRepository = reserbizRepository;
        }

        public async Task<Client> RegisterClient(Client client)
        {
            _reserbizRepository.ReserbizSystemRepository.Add(client);
            await _reserbizRepository.ReserbizSystemRepository.SaveAll();

            return client;
        }
    }
}