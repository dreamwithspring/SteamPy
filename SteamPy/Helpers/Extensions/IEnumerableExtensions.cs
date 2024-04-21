namespace steamPy.Helpers.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values)
        {
            return values == null || !values.Any();
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> values, bool flag, Func<T, bool> whereExpression)
        {
            if (!values.IsNullOrEmpty() && flag)
            {
                return values.Where(whereExpression);
            }
            return values;
        }
    }
}
