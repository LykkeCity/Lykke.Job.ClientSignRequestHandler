namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public class UnsignedTransaction : IUnsignedTransaction
    {
        public string Id { get; set; }
        public string Hex { get; set; }
        public string ClientId { get; set; }
    }
}