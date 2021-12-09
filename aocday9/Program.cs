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
        var currentPoint = new Point(x, y);
        var number = grid[x][y];
        
        var neighbours = GetNeighbours(currentPoint).Where(IsValidPoint).Select(p => grid[p.X][p.Y]).ToArray();

        if (number < neighbours.Min())
        {
            smallest.Add(number);
            lowestPoints.Add(currentPoint);
        }
    }
}

// solve part 2 using the lowpoints discovered above
List<HashSet<Point>> basins = new();

foreach (var lowPoint in lowestPoints)
{
    Queue<Point> pointsToCheck = new();
    HashSet<Point> checkedPoints = new();
    
    pointsToCheck.Enqueue(lowPoint);

    while (pointsToCheck.Count > 0)
    {
        var point = pointsToCheck.Dequeue();

        var neighbours = GetNeighbours(point).Where(IsValidPoint).ToArray();
        
        // for all the points that are valid
        foreach (var n in neighbours.Where(p => grid[p.X][p.Y] != 9).ToArray())
        {
            if (!checkedPoints.Contains(n))
                pointsToCheck.Enqueue(n); // add to queue as this isn't a point we've checked before

            // this is part of the basin, add it to the hashset
            checkedPoints.Add(n);
        }
    }
    
    basins.Add(checkedPoints);
}

var answerPart2 = basins.Select(b=> b.Count).OrderByDescending(b => b).Take(3).Aggregate((x, y) => x*y);
var answerPart1 = smallest.Sum() + smallest.Count;

Console.WriteLine(answerPart1);
Console.WriteLine(answerPart2);

if (answerPart1 != 585 || answerPart2 != 827904)
    throw new Exception();



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