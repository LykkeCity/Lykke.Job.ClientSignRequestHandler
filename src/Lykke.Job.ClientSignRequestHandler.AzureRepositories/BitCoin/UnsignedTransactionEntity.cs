using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Job.ClientSignRequestHandler.AzureRepositories.BitCoin
{
    public class UnsignedTransactionEntity : TableEntity, IUnsignedTransaction
    {
        public static string GeneratePartitionKey(string clientId)
        {
            return clientId;
        }

        public static string GenerateRowKey(string id)
        {
            return id;
        }

        public static UnsignedTransactionEntity Create(IUnsignedTransaction transaction)
        {
            return new UnsignedTransactionEntity
            {
                PartitionKey = GeneratePartitionKey(transaction.ClientId),
                RowKey = GenerateRowKey(transaction.Id),
                Id = transaction.Id,
                Hex = transaction.Hex,
                ClientId = transaction.ClientId
            };
        }

        public string Id { get; set; }
        public string Hex { get; set; }
        public string ClientId { get; set; }
    }
}