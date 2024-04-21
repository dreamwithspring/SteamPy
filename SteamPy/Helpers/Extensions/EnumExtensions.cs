using System.ComponentModel;

namespace steamPy.Helpers.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取Enum的Description注解
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            var type = value.GetType();

            var field = type.GetField(value.ToString());
            var customAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return (customAttr as DescriptionAttribute)?.Description ?? value.ToString();

        }



        public static T ToEnum<T>(this string? str, T? def = default(T)) where T : Enum
        {
            if (Enum.TryParse(typeof(T), str, out var result))
            {
                return (T)result;
            }
            return def;
        }
    }
}
