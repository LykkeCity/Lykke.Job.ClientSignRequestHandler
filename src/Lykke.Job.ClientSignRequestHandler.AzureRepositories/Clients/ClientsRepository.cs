using System.Collections.Generic;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients;

namespace Lykke.Job.ClientSignRequestHandler.AzureRepositories.Clients
{
    public class ClientsRepository : IClientAccountsRepository
    {
        private readonly INoSQLTableStorage<ClientAccountEntity> _clientsTablestorage;

        public ClientsRepository(INoSQLTableStorage<ClientAccountEntity> clientsTablestorage)
        {
            _clientsTablestorage = clientsTablestorage;
        }

        public async Task<IEnumerable<IClientAccount>> GetByIdAsync(string[] ids)
        {
            var partitionKey = ClientAccountEntity.GeneratePartitionKey();
            return await _clientsTablestorage.GetDataAsync(partitionKey, ids);
        }
    }
}