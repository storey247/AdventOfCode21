// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

var input = File.ReadLines("input.txt");

int[][] grid = input.ToArray().Select(x => x.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

List<int> smallest = new();

for (int x = 0; x < grid.Length; x++)
{
    for (int y = 0; y < grid[0].Length; y++)
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
    }
}

var answer = smallest.Sum() + smallest.Count;

Console.WriteLine(answer);
