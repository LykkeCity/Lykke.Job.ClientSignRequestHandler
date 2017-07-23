namespace Lykke.Job.ClientSignRequestHandler.Core
{
    public class AppSettings
    {
        public ClientSignRequestHandlerSettings ClientSignRequestHandlerJob { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }

        public class ClientSignRequestHandlerSettings
        {
            public DbSettings Db { get; set; }
            public NotificationsSettings Notifications { get; set; }
            public string[] LykkeAccounts { get; set; }
        }

        public class DbSettings
        {
            public string LogsConnString { get; set; }
            public string BitCoinQueueConnectionString { get; set; }
            public string ClientSignatureConnString { get; set; }
            public string ClientPersonalInfoConnString { get; set; }
        }

        public class NotificationsSettings
        {
            public string HubConnectionString { get; set; }
            public string HubName { get; set; }
        }

        public class SlackNotificationsSettings
        {
            public AzureQueueSettings AzureQueue { get; set; }

            public int ThrottlingLimitSeconds { get; set; }
        }

        public class AzureQueueSettings
        {
            public string ConnectionString { get; set; }

            public string QueueName { get; set; }
        }
    }
}