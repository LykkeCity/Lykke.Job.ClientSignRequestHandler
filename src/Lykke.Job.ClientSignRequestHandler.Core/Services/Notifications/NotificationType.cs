namespace Lykke.Job.ClientSignRequestHandler.Core.Services.Notifications
{
    public enum NotificationType
    {
        Info = 0,
        KycSucceess = 1,
        KycRestrictedArea = 2,
        KycNeedToFillDocuments = 3,
        TransctionFailed = 4,
        TransactionConfirmed = 5,
        AssetsCredited = 6,
        BackupWarning = 7,
        EthNeedTransactionSign = 8,
        PositionOpened = 9,
        PositionClosed = 10,
        MarginCall = 11,
        OffchainRequest = 12,
        NeedTransactionSign = 13,
        PushTxDialog = 14
    }
}