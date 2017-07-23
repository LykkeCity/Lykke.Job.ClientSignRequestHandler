using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Job.ClientSignRequestHandler.Core.Domain.Clients
{
    public interface IClientAccountsRepository
    {
        Task<IEnumerable<IClientAccount>> GetByIdAsync(string[] ids);
    }
}