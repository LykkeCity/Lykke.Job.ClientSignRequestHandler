using Newtonsoft.Json;

namespace Lykke.Job.ClientSignRequestHandler.Services.Notifications.Models
{
    public class AndoridPayloadNotification : IAndroidNotification
    {
        [JsonProperty("data")]
        public AndroidPayloadFields Data { get; set; }
    }
}