namespace aocextensions;

public static class StringExtensions
{
    public static bool IsAllLower(this string input)
        => input.All(char.IsLower);
    
    public static IEnumerable<string> ChunkString(this string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }
}