using System.Linq;
using System.Reflection;


namespace Tests.Helpers
{
    public class PropertyHelper<T>
    {
        public  PropertyInfo[] GetProperties()
        {
            return typeof(T).GetProperties();
        }

        public PropertyInfo? GetPropertyOfName(string name)
        {
            return GetProperties().FirstOrDefault(x => x.Name == name);
        }

        public PropertyInfo? GetPropertyOfNameCaseInsesitive(string name)
        {
            return GetProperties().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
