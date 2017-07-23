using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Job.ClientSignRequestHandler.Contract;
using Lykke.Job.ClientSignRequestHandler.Core;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.BitCoin;
using Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients;
using Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications;
using Lykke.JobTriggers.Triggers.Attributes;

namespace Lykke.Job.ClientSignRequestHandler.TriggerHandlers
{
    public class ClientSignRequestHandler
    {
        private readonly IUnsignedTransactionsRepository _unsignedTransactionsRepository;
        private readonly IBitCoinTransactionsRepository _bitCoinTransactionsRepository;
        private readonly ILog _log;
        private readonly AppSettings.ClientSignRequestHandlerSettings _settings;
        private readonly ISignedMultisigTransactionsSender _signedMultisigTransactionsSender;
        private readonly IAppNotifications _appNotifications;
        private readonly IClientAccountsRepository _clientAccountsRepository;

        public ClientSignRequestHandler(IUnsignedTransactionsRepository unsignedTransactionsRepository, IBitCoinTransactionsRepository bitCoinTransactionsRepository,
            ILog log, AppSettings.ClientSignRequestHandlerSettings settings, ISignedMultisigTransactionsSender signedMultisigTransactionsSender, 
            IAppNotifications appNotifications, IClientAccountsRepository clientAccountsRepository)
        {
            _unsignedTransactionsRepository = unsignedTransactionsRepository;
            _bitCoinTransactionsRepository = bitCoinTransactionsRepository;
            _log = log;
            _settings = settings;
            _signedMultisigTransactionsSender = signedMultisigTransactionsSender;
            _appNotifications = appNotifications;
            _clientAccountsRepository = clientAccountsRepository;
        }

        [QueueTrigger("transaction-out", 1000)]
        public async Task ProcessInMessage(TransactionToSignMsg msg)
        {
            var msgJson = msg.ToJson();
            var logTask = _log.WriteInfoAsync(nameof(ClientSignRequestHandler), nameof(ProcessInMessage), msgJson, "Message received");

            try
            {
                var tx = await _bitCoinTransactionsRepository.FindByTransactionIdAsync(msg.TransactionId.ToString());

                if (tx == null)
                {
                    await _log.WriteWarningAsync(nameof(ClientSignRequestHandler), nameof(ProcessInMessage), msgJson, "No tx found");
                    throw new Exception("No tx found");
                }

                var context = tx.GetBaseContextData();

                var clientAccountsDict = (await _clientAccountsRepository.GetByIdAsync(context.SignsClientIds)).ToDictionary(x => x.Id);

                foreach (var id in context.SignsClientIds)
                {
                    if (_settings.LykkeAccounts.Contains(id))
                    {
                        await _signedMultisigTransactionsSender.SendTransaction(new SignedTransaction
                        {
                            TransactionId = msg.TransactionId,
                            Transaction = msg.Transaction
                        });
                    }
                    else
                    {
                        await _unsignedTransactionsRepository.InsertAsync(new UnsignedTransaction
                        {
                            ClientId = id,
                            Hex = msg.Transaction,
                            Id = msg.TransactionId.ToString()
                        });

                        await _appNotifications.SendDataNotificationToAllDevicesAsync(new[] { clientAccountsDict[id].NotificationsId }, NotificationType.NeedTransactionSign, "Wallet");
                    }
                }
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(ClientSignRequestHandler), nameof(ProcessInMessage), msg.ToJson(), ex);
            }
            finally
            {
                await logTask;
            }
        }
    }
}