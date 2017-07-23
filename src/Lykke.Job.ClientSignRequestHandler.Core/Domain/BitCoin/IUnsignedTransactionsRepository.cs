using System.Threading.Tasks;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public interface IUnsignedTransactionsRepository
    {
        Task InsertAsync(IUnsignedTransaction transaction);
    }
}