using System;
using System.Collections.Generic;
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
    public class ClientRepository
        : BaseRepository<Client>, IClientRepository<Client>
    {
        public ClientRepository(IReserbizRepository<Client> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {

        }

        public async Task<Client> RegisterClient(Client client)
        {
            string dbHashName;

            CreateHashDBName(client.DBName, out dbHashName);

            client.DBHashName = dbHashName;

            await AddEntity(client);

            return client;
        }

        public async Task<Client> GetCompanyInfoByName(string companyName)
        {
            var companyInfo = await _reserbizRepository.SystemDbContext.Clients.FirstOrDefaultAsync(c => c.Name == companyName);

            return companyInfo;
        }

        private void CreateHashDBName(string dbName, out string dbHashName)
        {
            dbHashName = SHA1Util.SHA1HashStringForUTF8String(dbName);
        }
    }
}