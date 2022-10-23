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
        private readonly IOptions<AppHostInfo> _appHostInfo;
        private readonly IOptions<ApplicationSettings> _applicationSettings;

        public ClientDbManagerRepository(
            IReserbizRepository<IEntity> reserbizRepository,
            IOptions<AppHostInfo> appHostInfo,
            IOptions<ApplicationSettings> applicationSettings)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {
            _appHostInfo = appHostInfo;
            _applicationSettings = applicationSettings;
        }

        public async Task SyncAllClientDatabases()
        {
            var activeDatabases = await _reserbizRepository.SystemDbContext.Clients
                .Where(a => a.IsActive == true && a.IsDelete == false)
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
                var url = String.Format("{0}{1}", _appHostInfo.Value.Domain, _applicationSettings.Value.AppSettingsURL.SyncDatabaseURL);
                var httpClient = new RestClient(url);
                httpClient.Options.MaxTimeout = -1;
                var httpRequest = new RestRequest(url, Method.Post);
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