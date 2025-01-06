namespace Sabim.Services.Contracts
{
    public interface ILoggerService
    {
        void LogError(string message);
        void LogWarning(string message);
        void LogInfo(string message);
        void LogDebug(string message);
    }
}
