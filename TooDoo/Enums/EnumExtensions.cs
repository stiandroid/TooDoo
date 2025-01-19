using System.ComponentModel;
using System.Reflection;

namespace TooDoo.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo != null)
        {
            var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }
}
