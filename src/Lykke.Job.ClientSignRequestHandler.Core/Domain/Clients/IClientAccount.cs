using System;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients
{
    public interface IClientAccount
    {
        DateTime Registered { get; }
        string Id { get; }
        string Email { get; }
        string PartnerId { get; }
        string Phone { get; }
        string Pin { get; }
        string NotificationsId { get; }
    }
}