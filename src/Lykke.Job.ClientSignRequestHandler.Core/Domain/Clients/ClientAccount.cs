using System;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients
{
    public class ClientAccount : IClientAccount
    {
        public DateTime Registered { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pin { get; set; }
        public string NotificationsId { get; set; }
        public string PartnerId { get; set; }
        public bool IsReviewAccount { get; set; }
    }
}