
namespace Lykke.Job.ClientSignRequestHandler.Core.Services
{
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        string GetHealthWarningMessage();
    }
}