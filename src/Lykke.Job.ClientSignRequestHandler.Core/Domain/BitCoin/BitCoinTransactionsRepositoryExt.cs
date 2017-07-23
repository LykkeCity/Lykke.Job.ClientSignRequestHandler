namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin
{
    public static class BitCoinTransactionsRepositoryExt
    {
        public static BaseContextData GetBaseContextData(this IBitcoinTransaction src)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BaseContextData>(src.ContextData);
        }
    }
}