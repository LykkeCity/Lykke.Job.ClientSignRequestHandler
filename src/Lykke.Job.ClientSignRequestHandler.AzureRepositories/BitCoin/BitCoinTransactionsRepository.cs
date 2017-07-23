using System.Threading.Tasks;
using AzureStorage;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;

namespace Lykke.Job.ClientSignRequestHandler.AzureRepositories.BitCoin
{
    public class BitCoinTransactionsRepository : IBitCoinTransactionsRepository
    {
        private readonly INoSQLTableStorage<BitCoinTransactionEntity> _tableStorage;

        public BitCoinTransactionsRepository(INoSQLTableStorage<BitCoinTransactionEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<IBitcoinTransaction> FindByTransactionIdAsync(string transactionId)
        {
            var partitionKey = BitCoinTransactionEntity.ByTransactionId.GeneratePartitionKey();
            var rowKey = BitCoinTransactionEntity.ByTransactionId.GenerateRowKey(transactionId);
            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }
    }
}