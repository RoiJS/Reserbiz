using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Helpers;
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
            string dbHashName;

            CreateHashDBName(client.DBName, out dbHashName);

            client.DBHashName = dbHashName;

            _reserbizRepository.SystemRepository.AddEntity(client);

            await _reserbizRepository.SystemRepository.SaveChangesAsync();

            return client;
        }

        public async Task<bool> ClientExists(string name, string dbName)
        {
            if (await _reserbizRepository.SystemRepository.Context.Clients.AnyAsync(c => c.Name == name && c.DBName == dbName))
                return true;

            return false;
        }

        public async Task<Client> GetCompanyInfoByName(string companyName)
        {
            var companyInfo = await _reserbizRepository.SystemRepository.Context.Clients.FirstOrDefaultAsync(c => c.Name == companyName);

            return companyInfo;
        }

        private void CreateHashDBName(string dbName, out string dbHashName)
        {
            dbHashName = SHA1Util.SHA1HashStringForUTF8String(dbName);
        }
    }
}