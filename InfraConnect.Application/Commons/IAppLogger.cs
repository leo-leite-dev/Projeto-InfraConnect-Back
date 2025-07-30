namespace InfraConnect.Application.Commons
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message);
        void LogWarning(Exception ex, string message);
        void LogError(Exception ex, string message);
    }
}