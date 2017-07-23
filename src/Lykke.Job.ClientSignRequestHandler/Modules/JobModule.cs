using System;
using Autofac;
using AzureStorage.Queue;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Job.ClientSignRequestHandler.AzureRepositories.BitCoin;
using Lykke.Job.ClientSignRequestHandler.AzureRepositories.Clients;
using Lykke.Job.ClientSignRequestHandler.Core;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients;
using Lykke.Job.ClientSignRequestHandler.Core.Services;
using Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications;
using Lykke.Job.ClientSignRequestHandler.Services;
using Lykke.Job.ClientSignRequestHandler.Services.Notifications;

namespace Lykke.Job.ClientSignRequestHandler.Modules
{
    public class JobModule : Module
    {
        private readonly AppSettings.ClientSignRequestHandlerSettings _settings;
        private readonly ILog _log;

        public JobModule(AppSettings.ClientSignRequestHandlerSettings settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_settings)
                .SingleInstance();

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(TimeSpan.FromSeconds(30)));

            // NOTE: You can implement your own poison queue notifier. See https://github.com/LykkeCity/JobTriggers/blob/master/readme.md
            // builder.Register<PoisionQueueNotifierImplementation>().As<IPoisionQueueNotifier>();

            RegisterRepositories(builder);
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.Register<IAppNotifications>(x => new SrvAppNotifications(_settings.Notifications.HubConnectionString, _settings.Notifications.HubName))
                .SingleInstance();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterInstance<IBitCoinTransactionsRepository>(
                new BitCoinTransactionsRepository(
                    new AzureTableStorage<BitCoinTransactionEntity>(_settings.Db.BitCoinQueueConnectionString, "BitCoinTransactions", _log)));

            builder.RegisterInstance<ISignedMultisigTransactionsSender>(
                new SignedMultisigTransactionsSender(
                    new AzureQueueExt(_settings.Db.ClientSignatureConnString, "client-signed-transactions")));

            builder.RegisterInstance<IUnsignedTransactionsRepository>(
                new UnsignedTransactionsRepository(
                    new AzureTableStorage<UnsignedTransactionEntity>(_settings.Db.BitCoinQueueConnectionString, "UnsignedTransactions", _log)));

            builder.RegisterInstance<IClientAccountsRepository>(
                new ClientsRepository(
                    new AzureTableStorage<ClientAccountEntity>(_settings.Db.ClientPersonalInfoConnString, "Traders", _log)));
        }
    }
}