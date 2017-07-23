using System;

namespace Lykke.Job.ClientSignRequestHandler.Contract
{
    public class TransactionToSignMsg
    {
        public Guid TransactionId { get; set; }
        public string Transaction { get; set; }
    }
}