using System.Threading.Tasks;
using AzureStorage;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;

namespace Lykke.Job.ClientSignRequestHandler.AzureRepositories.BitCoin
{
    public class UnsignedTransactionsRepository : IUnsignedTransactionsRepository
    {
        private readonly INoSQLTableStorage<UnsignedTransactionEntity> _tableStorage;

        public UnsignedTransactionsRepository(INoSQLTableStorage<UnsignedTransactionEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task InsertAsync(IUnsignedTransaction transaction)
        {
            var entity = UnsignedTransactionEntity.Create(transaction);
            await _tableStorage.InsertAsync(entity);
        }
    }
}