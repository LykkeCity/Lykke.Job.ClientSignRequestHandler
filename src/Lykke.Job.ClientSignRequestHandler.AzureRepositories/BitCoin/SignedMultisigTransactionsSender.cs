using System.Threading.Tasks;
using AzureStorage.Queue;
using Common;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;

namespace Lykke.Job.ClientSignRequestHandler.AzureRepositories.BitCoin
{
    public class SignedMultisigTransactionsSender : ISignedMultisigTransactionsSender
    {
        private readonly IQueueExt _queueExt;

        public SignedMultisigTransactionsSender(IQueueExt queueExt)
        {
            _queueExt = queueExt;
        }

        public async Task SendTransaction(SignedTransaction transaction)
        {
            await _queueExt.PutRawMessageAsync(transaction.ToJson());
        }
    }
}