// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;

var input = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToArray();

var min = input.Min();
var max = input.Max();

var answer = max;
long abstotal = long.MaxValue;

for (var i = min; i <= max; i++)
{
    long total = 0;

    foreach (var sub in input)
    {
        var diff = Math.Abs(sub - i);
        var fuel = CalcFuel(diff);
        total += fuel;
    }

    if (total < abstotal)
    {
        answer = i;
        abstotal = total;
    }
}

Console.WriteLine($"{answer} {abstotal}");

long CalcFuel(int input)
{
    return input * (input + 1) / 2;
}