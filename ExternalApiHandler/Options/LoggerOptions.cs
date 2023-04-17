using Logger;

namespace ExternalApiHandler.Options
{
    internal class LoggerOptions
    {
        public MinimalLoggerLevel LoggerLevel { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
