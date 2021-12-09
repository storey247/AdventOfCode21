// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

var input = File.ReadLines("input.txt");

int[][] grid = input.ToArray().Select(x => x.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

List<int> smallest = new();
List<Point> lowestPoints = new();

for (var x = 0; x < grid.Length; x++)
{
    for (var y = 0; y < grid[0].Length; y++)
    {
        var current = grid[x][y];
        //checkLeft
        if (x != 0 && current >= grid[x - 1][y])
            continue;
        //checkRight
        if (x != grid.Length - 1 && current >= grid[x + 1][y])
            continue;
        //checkTop
        if (y != 0 && current >= grid[x][y - 1])
            continue;
        //checkBot
        if (y != grid[0].Length - 1 && current >= grid[x][y + 1])
            continue;
        smallest.Add(current);
        lowestPoints.Add(new Point(x, y));
    }
}

List<HashSet<Point>> basins = new();

foreach (var lowPoint in lowestPoints)
{
    Queue<Point> pointsToCheck = new();
    HashSet<Point> checkedPoints = new();
    
    pointsToCheck.Enqueue(lowPoint);

    while (pointsToCheck.Count > 0)
    {
        var point = pointsToCheck.Dequeue();
        checkedPoints.Add(point);

        var neighbours = GetNeighbours(point).Where(IsValidPoint).ToArray();
        
        // check if the points are valid
        foreach (var n in neighbours)
        {
            if (grid[n.X][n.Y] != 9)
            {
                if (!checkedPoints.Contains(n))
                {
                    pointsToCheck.Enqueue(n);
                }
                
                checkedPoints.Add(n);
            }
        }
    }
    
    basins.Add(checkedPoints);
}

var answerPart2 = basins.Select(b=> b.Count).OrderByDescending(b => b).Take(3).Aggregate((x, y) => x*y);
var answerPart1 = smallest.Sum() + smallest.Count;

Console.WriteLine(answerPart1);
Console.WriteLine(answerPart2);

List<Point> GetNeighbours(Point val)
{
    return new List<Point>
    {
        new(val.X - 1, val.Y),
        new(val.X + 1, val.Y),
        new(val.X, val.Y - 1),
        new(val.X, val.Y + 1),
    };
}

bool IsValidPoint(Point point)
{
    return point.X >= 0 
           && point.Y >= 0 
           && point.X < grid.Length 
           && point.Y < grid[0].Length;
}