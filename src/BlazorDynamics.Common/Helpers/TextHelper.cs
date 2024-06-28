namespace BlazorDynamics.Common.Helpers
{
    internal class TextHelper
    {
        private TextHelper()
        {
        }

        internal static String FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }

        internal static String FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsUpper(input[0]))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }
    }
}
