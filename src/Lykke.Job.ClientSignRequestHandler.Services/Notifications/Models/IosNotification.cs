using Newtonsoft.Json;

namespace Lykke.Job.ClientSignRequestHandler.Services.Notifications.Models
{
    public class IosNotification : IIosNotification
    {
        [JsonProperty("aps")]
        public IosFields Aps { get; set; }
    }
}