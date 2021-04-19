using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using RestSharp;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ClientDbManagerRepository
        : BaseRepository<IEntity>, IClientDbManagerRepository<IEntity>
    {
        private readonly IOptions<AppSettingsURL> _appSettingsUrl;
        public ClientDbManagerRepository(IReserbizRepository<IEntity> reserbizRepository, IOptions<AppSettingsURL> _appSettingsUrl) : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {
            this._appSettingsUrl = _appSettingsUrl;
        }

        public async Task SyncAllClientDatabases()
        {
            var activeDatabases = await _reserbizRepository.SystemDbContext.Clients
                .Where(a => a.IsDelete == false)
                .ToListAsync();

            foreach (var db in activeDatabases)
            {
                await SendRequestToSyncDatabase(db);
            }
        }

        private async Task SendRequestToSyncDatabase(Client client)
        {
            try
            {
                var httpClient = new RestClient(_appSettingsUrl.Value.SyncDatabaseURL);
                httpClient.Timeout = -1;
                var httpRequest = new RestRequest(Method.POST);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                await httpClient.ExecuteAsync(httpRequest);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}