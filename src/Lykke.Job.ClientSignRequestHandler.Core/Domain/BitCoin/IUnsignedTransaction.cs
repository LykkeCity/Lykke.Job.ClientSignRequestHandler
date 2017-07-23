namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public interface IUnsignedTransaction
    {
        string Id { get; set; }
        string Hex { get; set; }
        string ClientId { get; set; }
    }
}
