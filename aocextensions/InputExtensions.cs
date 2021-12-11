using System.Drawing;
using System.IO;
using System.Linq;

namespace aocextensions
{
    public static class InputExtensions
    {
        public static char[][] GetInputLineAsChar(this string inputFile)
            => File.ReadLines(inputFile).Select(x => x.ToArray()).ToArray();
        
        public static int[][] GetAllInputAsInt(this string inputFile)
            => File.ReadAllLines("input.txt").Select(x => x.ToArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
        
    }
}