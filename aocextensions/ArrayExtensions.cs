namespace aocextensions;

public static class ArrayExtensions
{
    public static int[][] IncrementAllByValue(this int[][] input, int value)
    {
       return input.Select(x => x.Select(y => y + value).ToArray()).ToArray();
    }
}