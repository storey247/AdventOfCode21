namespace aocextensions;

public static class StringExtensions
{
    public static bool IsAllLower(this string input)
        => input.All(char.IsLower);
}