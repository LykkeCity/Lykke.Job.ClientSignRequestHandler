using System.Threading.Tasks;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public interface IBitCoinTransactionsRepository
    {
        Task<IBitcoinTransaction> FindByTransactionIdAsync(string transactionId);
    }
}