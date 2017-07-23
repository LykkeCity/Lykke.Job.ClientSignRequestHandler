using System.Threading.Tasks;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public interface ISignedMultisigTransactionsSender
    {
        Task SendTransaction(SignedTransaction transaction);
    }
}