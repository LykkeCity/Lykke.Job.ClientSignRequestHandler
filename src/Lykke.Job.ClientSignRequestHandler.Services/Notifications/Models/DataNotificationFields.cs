using Newtonsoft.Json;

namespace Lykke.Job.ClientSignRequestHandler.Services.Notifications.Models
{
    public class DataNotificationFields : IosFields
    {
        [JsonProperty("content-available")]
        public int ContentAvailable { get; set; } = 1;
    }
}