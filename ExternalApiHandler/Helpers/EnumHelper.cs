namespace ExternalApiHandler.Helpers
{
    public class EnumHelper
    {
        public static T GetEnumValueFromKey<T>(string key) where T : Enum
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The key cannot be null or empty");
            }

            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{enumType} is not an enumerated type");
            }

            var enumNames = Enum.GetNames(enumType);
            foreach (var enumName in enumNames)
            {
                if (string.Equals(enumName, key, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)Enum.Parse(enumType, enumName);
                }
            }

            throw new ArgumentException($"No enum value found for key {key}");
        }
    }
}
