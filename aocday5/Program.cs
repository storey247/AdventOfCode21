// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
    
int[,] result = new int[999, 999];
int answer = 0;

var inputs = File.ReadAllLines("input.txt").Select(s => s.Split(new[] {" -> ", ","}, StringSplitOptions.RemoveEmptyEntries)).ToArray();

var lines = inputs.Where(IsLine).ToArray();

foreach (var input in lines)
{
    var x1 =  int.Parse(input[0]);
    var x2 = int.Parse(input[2]);
    var y1 = int.Parse(input[1]);
    var y2 = int.Parse(input[3]);
    
    if (x1 == x2)
        DrawVertical(Math.Min(y1, y2), Math.Max(y1, y2), x1);

    if (y1 == y2)
        DrawHorizontal(Math.Min(x1, x2), Math.Max(x1, x2), y1);
}

var diagonals = inputs.Where(i => !IsLine(i)).ToArray();

foreach (var input in diagonals)
{
    var x1 =  int.Parse(input[0]);
    var x2 = int.Parse(input[2]);
    var y1 = int.Parse(input[1]);
    var y2 = int.Parse(input[3]);

    DrawDiagonal(x1, x2, y1, y2);
}

foreach (var val in result)
{
    if (val > 1)
        answer++;
}


Console.WriteLine(answer);

bool IsLine(string[] input)
{
    return input[0] == input[2] || input[1] == input[3];
}

void DrawHorizontal(int x1, int x2, int y)
{
    for (int i = x1; i <= x2; i++)
    {
        result[y, i]++;
    }
}

void DrawVertical(int y1, int y2, int x)
{
    for (int i = y1; i <= y2; i++)
    {
        result[i, x]++;
    }
}

void DrawDiagonal(int x1, int x2, int y1, int y2)
{
    var times = Math.Abs(x1 - x2);
    
    for (int i = 0; i <= times; i++)
    {
        result[y1, x1]++;

        x1 = x1 > x2 ? x1 - 1 : x1 + 1;
        y1 = y1 > y2 ? y1 - 1 : y1 + 1;
    }

    Console.WriteLine("test");
}