using System.Threading.Tasks;

namespace Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications
{
    public interface IAppNotifications
    {
        Task SendDataNotificationToAllDevicesAsync(string[] notificationIds, NotificationType type, string entity, string id = "");
    }
}