using Logger;

namespace ExternalApiCrawler.Helpers
{
    public class ExceptionHelper
    {
        public static void LogAndThrow<T>(string errorMessage, ILogger? logger = null) where T : Exception
        {
            logger?.Error(errorMessage);
            throw Activator.CreateInstance(typeof(T), errorMessage) as T ?? new Exception(errorMessage);
        }
    }
}
