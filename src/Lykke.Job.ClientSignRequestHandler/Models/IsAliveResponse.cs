
namespace Lykke.Job.ClientSignRequestHandler.Models
{
    public class IsAliveResponse
    {
        public string Version { get; set; }
        public string Env { get; set; }
        public string HealthWarning { get; set; }

    }
}