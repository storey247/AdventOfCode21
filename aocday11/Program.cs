// See https://aka.ms/new-console-template for more information

using System.Drawing;
using aocextensions;

var input = InputExtensions.GetAllInputAsInt("input.txt");
var answer1 = 0;

// part 1
for (int i = 0; i < 100; i++)
{
    // increase everything by 1
    input = input.IncrementAllByValue(1);

    var flashes = GetDailyFlash(input);
    Console.WriteLine($"Flashes today: {flashes}");
    answer1 += flashes;

    // reset all the flashed octopods
    input = input.Select(x => x.Select(y => y = y > 9 ? 0 : y).ToArray()).ToArray();
    
}

var answer2 = 0;

//part 2
input = InputExtensions.GetAllInputAsInt("input.txt");
for (int i = 0; i < 100000; i++)
{
    // increase everything by 1
    input = input.IncrementAllByValue(1);

    GetDailyFlash(input);
    // reset all the flashed octopods
    input = input.Select(x => x.Select(y => y = y > 9 ? 0 : y).ToArray()).ToArray();

    if (input.Sum(p => p.Sum()) == 0)
    {
        answer2 = i + 1;
        Console.WriteLine($"All flashed on day: {i + 1}");
        break;
    }
}

Console.WriteLine($"Answer1: {answer1}");
if (answer1 != 1634)
    throw new Exception();

Console.WriteLine($"Answer2: {answer2}");
if (answer2 != 210)
    throw new Exception();

int GetDailyFlash(int[][] dailyInput)
{
    var flashes = 0;
    bool someoneFlash = true;
    while (someoneFlash)
    {
        someoneFlash = false;
        for (int row = 0; row < dailyInput.Length; row++)
        {
            for (int col = 0; col < dailyInput[row].Length; col++)
            {
                if (dailyInput[row][col] == 10)
                {
                    // this octopod wants to flash
                    flashes++;
                    dailyInput[row][col]++;
                    someoneFlash = true;

                    // power up the neighbours
                    foreach (var value in new Point(row, col).GetAllValidNeighbours(dailyInput.Length, dailyInput[0].Length))
                    {
                        if (dailyInput[value.X][value.Y] < 10)
                        {
                            dailyInput[value.X][value.Y]++;
                        }
                    }
                }
            }
        }
    }

    return flashes;
}