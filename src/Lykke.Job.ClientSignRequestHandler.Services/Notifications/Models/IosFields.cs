using Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications;
using Newtonsoft.Json;

namespace Lykke.Job.ClientSignRequestHandler.Services.Notifications.Models
{
    public class IosFields
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }
        [JsonProperty("type")]
        public NotificationType Type { get; set; }
    }
}