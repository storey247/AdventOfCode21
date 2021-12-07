// See https://aka.ms/new-console-template for more information


using System;
using System.IO;
using System.Linq;
using System.Security;

var input = File.ReadAllLines("input.txt");

var horizontal = 0;
var depth = 0;
var aim = 0;

foreach (var command in input)
{
    var instruction = command.Split(' ')[0];
    var value = int.Parse(command.Split(' ')[1]);
    
    switch (instruction)
    {
        case "up":
            aim -= value;
            break;
        case "down":
            aim += value;
            break;
        case "forward":
            horizontal += value;
            depth += (aim * value);
            break;
    }
}


Console.WriteLine(horizontal * depth);