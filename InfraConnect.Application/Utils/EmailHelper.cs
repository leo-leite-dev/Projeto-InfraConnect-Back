namespace InfraConnect.Application.Utils
{
    public static class EmailHelper
    {
        public static bool IsEmail(string input)
        {
            return !string.IsNullOrWhiteSpace(input)
                && input.Contains("@")
                && input.Contains(".");
        }
    }
}