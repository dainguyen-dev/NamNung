namespace NamNung.Web.Extensions
{
    public static class StringExtension
    {
        public static bool HasText(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return true;
        }
    }
}