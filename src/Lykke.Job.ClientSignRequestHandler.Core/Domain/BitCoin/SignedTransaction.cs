using System;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public class SignedTransaction
    {
        public Guid? TransactionId { get; set; }
        public string Transaction { get; set; }
    }
}