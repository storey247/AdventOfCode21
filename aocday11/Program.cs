// See https://aka.ms/new-console-template for more information

using System.Drawing;

var input = File.ReadAllLines("input.txt").Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
var total = 0;

for (int i = 0; i < 100; i++)
{
    // increase everything by 1
    input = input.Select(x => x.Select(y => y + 1).ToArray()).ToArray();
    
    var flashes = 0;
    bool someoneFlash = true;
    while (someoneFlash)
    {
        someoneFlash = false;
        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == 10)
                {
                    // this octopod wants to flash
                    flashes++;
                    input[row][col]++;
                    someoneFlash = true;

                    foreach (var value in GetNeighbours(new Point(row, col)))
                    {
                        if (input[value.X][value.Y] < 10)
                        {
                            input[value.X][value.Y]++;
                        }
                    }
                }
            }
        }
    }
    
    Console.WriteLine($"Flashes today: {flashes}");
    total += flashes;

    // reset all the flashed octopods
    input = input.Select(x => x.Select(y => y = y > 9 ? 0 : y).ToArray()).ToArray();

    if (input.Sum(p => p.Sum()) == 0)
    {
        Console.WriteLine($"All flashed on day: {i + 1}");
        break;
    }
}

Console.WriteLine(total);


List<Point> GetNeighbours(Point val)
{
    var neighbours = new List<Point>
    {
        new(val.X - 1, val.Y),
        new(val.X + 1, val.Y),
        new(val.X, val.Y - 1),
        new(val.X, val.Y + 1),
        new(val.X - 1, val.Y - 1),
        new(val.X + 1, val.Y + 1),
        new(val.X - 1, val.Y + 1),
        new(val.X + 1, val.Y - 1),
    };

    return neighbours.Where(x =>
    {
        return x.X >= 0 
               && x.Y >= 0 
               && x.X < input.Length 
               && x.Y < input[0].Length;
    }).ToList();
}

