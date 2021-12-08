// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");

var outputValues = input.Select(i => i.Split("|", StringSplitOptions.TrimEntries)[1]);

var uniqueLengths = new[] {2, 3, 4, 7};

var answer = 0;

foreach (var value in outputValues)
{
    var output = value.Split();
    answer += output.Count(o => uniqueLengths.Contains(o.Length));
}

Console.WriteLine(answer);