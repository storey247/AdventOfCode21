using System.IO;
using System.Linq;

namespace aocextensions
{
    public static class InputExtensions
    {
        public static int[] GetInputLineAsInt(this string inputFile)
            => File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToArray();
        
        public static int[] GetAllInputAsInt(this string inputFile)
            => File.ReadAllLines("input.txt").Select(int.Parse).ToArray();

    }
}