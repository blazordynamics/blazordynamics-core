using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorDynamics.Common.Helpers
{
    public class TokenReplacer
    {
        public TokenReplacer()
        {
            
        }

        public static string ReplaceTokens<T>(string template, T obj)
        {
            if (EqualityComparer<T>.Default.Equals(obj, default)) { throw new ArgumentNullException(nameof(obj)); }
            if (template == null) return string.Empty;
            // Regex to find tokens in the format {PropertyName}
            var tokenRegex = new Regex(@"\{(?<token>[^\}]+)\}", RegexOptions.None, TimeSpan.FromMilliseconds(100));

            return tokenRegex.Replace(template, match =>
            {
                // Extract the property name from the match
                string propertyName = match.Groups["token"].Value;

                // Get the property from the object
                PropertyInfo? property = typeof(T).GetProperty(propertyName);

                if (property != null)
                {
                    // Get the value of the property and convert it to string
                    object? value = property.GetValue(obj);
                    return value?.ToString() ?? "";
                }

                // If property is not found, return the original match
                return match.Value;
            });
        }
    }
}
