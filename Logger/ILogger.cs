using System.Diagnostics;

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
        string SetLogPath(string path);
        string SetFileName(string fileName);

        // Methods for fluent API to manage path
        ILogger AddToPath(string directoryName);
        ILogger GoBackToParentDirectory();
    }
}