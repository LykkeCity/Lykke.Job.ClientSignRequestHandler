using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications;
using Lykke.Job.ClientSignRequestHandler.Services.Notifications.Models;

namespace Lykke.Job.ClientSignRequestHandler.Services.Notifications
{
    public class SrvAppNotifications : IAppNotifications
    {
        private readonly string _connectionString;
        private readonly string _hubName;

        public SrvAppNotifications(string connectionString, string hubName)
        {
            _connectionString = connectionString;
            _hubName = hubName;
        }

        public async Task SendDataNotificationToAllDevicesAsync(string[] notificationIds, NotificationType type, string entity, string id = "")
        {
            var apnsMessage = new IosNotification
            {
                Aps = new DataNotificationFields
                {
                    Type = type
                }
            };

            var gcmMessage = new AndoridPayloadNotification
            {
                Data = new AndroidPayloadFields
                {
                    Entity = EventsAndEntities.GetEntity(type),
                    Event = EventsAndEntities.GetEvent(type),
                    Id = id
                }
            };

            await SendIosNotificationAsync(notificationIds, apnsMessage);
            await SendAndroidNotificationAsync(notificationIds, gcmMessage);
        }

        private async Task SendIosNotificationAsync(string[] notificationIds, IIosNotification notification)
        {
            await SendRawNotificationAsync(Device.Ios, notificationIds, notification.ToJson(ignoreNulls: true));
        }

        private async Task SendAndroidNotificationAsync(string[] notificationIds, IAndroidNotification notification)
        {
            await SendRawNotificationAsync(Device.Android, notificationIds, notification.ToJson(ignoreNulls: true));
        }

        private async Task SendRawNotificationAsync(Device device, string[] notificationIds, string payload)
        {
            try
            {
                notificationIds = notificationIds?.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (notificationIds != null && notificationIds.Any())
                {
                    var hub = CustomNotificationHubClient.CreateClientFromConnectionString(_connectionString, _hubName);

                    if (device == Device.Ios)
                        await hub.SendAppleNativeNotificationAsync(payload, notificationIds);
                    else
                        await hub.SendGcmNativeNotificationAsync(payload, notificationIds);
                }
            }
            catch (Exception)
            {
                //TODO: process exception
            }
        }
    }
}