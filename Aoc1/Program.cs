// See https://aka.ms/new-console-template for more information

using System;
using System.IO;

var input = File.ReadAllLines("input.txt");

var count = 0;
var last = int.Parse( input[0]);

for (var i = 1; i < input.Length; i++)
{
    var number = int.Parse(input[i]);
    if (number > last)
    {
        count++;
    }

    last = number;
}

Console.WriteLine(count);