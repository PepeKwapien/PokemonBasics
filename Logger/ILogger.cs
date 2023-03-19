namespace Logger
{
    public interface ILogger
    {
        // Methods to log messages
        bool Debug(string message);
        bool Info(string message);
        bool Warn(string message);
        bool Success(string message);
        bool Error(string message);

        // Methods to fully set path
        void SetPath(string path);
        void SetFileName(string fileName);
    }
}