namespace Logger
{
    public interface ILogger
    {
        // Methods to log messages
        bool Debug(string message);
        bool Info(string message);
        bool Warn(string message);
        bool Error(string message);
    }
}