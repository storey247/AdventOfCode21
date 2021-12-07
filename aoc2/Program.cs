// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(i => int.Parse(i)).ToArray();

var number = input[0] + input[1] + input[2];
var count = 0;

for (var i = 1; i < input.Length - 2; i++)
{
    var group = input[i] + input[i + 1] + input[i + 2];

    if (group > number)
        count++;

    number = group;
}

Console.WriteLine(count);