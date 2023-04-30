using Logger;

namespace ExternalApiCrawler.Helpers
{
    public class EnumHelper
    {
        public static T GetEnumValueFromKey<T>(string key, ILogger? logger = null) where T : Enum
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                ExceptionHelper.LogAndThrow<ArgumentException>("The key cannot be null or empty", logger);
            }

            var enumType = typeof(T);

            var enumNames = Enum.GetNames(enumType);
            foreach (var enumName in enumNames)
            {
                if (string.Equals(enumName, key, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)Enum.Parse(enumType, enumName);
                }
            }

            ExceptionHelper.LogAndThrow<ArgumentException>($"No enum value found for key {key}", logger);
            throw new Exception("I had to because compiler thinks that I do not return value here");
        }
    }
}
