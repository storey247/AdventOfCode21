// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var validOpeners = new HashSet<char> {'(', '[', '<', '{'};
var validClosers = new HashSet<char> {')', ']', '>', '}'};

var input = File.ReadAllLines("input.txt").Select(x => x.ToCharArray()).ToArray();

List<char> invalidSyntax = new();
List<Stack<char>> incompleteLines = new();

foreach (var row in input)
{
    var syntaxStack = new Stack<char>();
    
    foreach (var c in row)
    {
        if (validOpeners.Contains(c))
        {
            syntaxStack.Push(c);
            continue;
        }

        if (validClosers.Contains(c))
        {
            var expectedCloser = CalcExpectedCloser(syntaxStack.Peek());
            if (c != expectedCloser)
            {
                invalidSyntax.Add(c);
                syntaxStack.Clear();
                break;
            }

            syntaxStack.Pop();
        }
    }
    
    if (syntaxStack.Any())
        incompleteLines.Add(syntaxStack);
}

var scorer = BuildScorer();
long answer = 0;

foreach (var invalid in invalidSyntax)
    answer += scorer[invalid];

var incompleteScorer = BuildIncompleteScorer();
var scores = new List<long>();
foreach (var incompleteLine in incompleteLines)
{
    long total = 0;

    while (incompleteLine.Count > 0)
    {
        var expected = CalcExpectedCloser(incompleteLine.Pop());
        total = total * 5 + incompleteScorer[expected];
    }
    
    scores.Add(total);
}

scores.Sort();
var answer2 = scores[scores.Count / 2];

Console.WriteLine(answer);
Console.WriteLine(answer2);

char CalcExpectedCloser(char opener)
{
    switch (opener)
    {
        case '(':
            return ')';
        case '[':
            return ']';
        case '<':
            return '>';
        case '{':
            return '}';
        default:
            throw new Exception();
    }
}

Dictionary<char, int> BuildScorer()
{
    return new Dictionary<char, int>()
    {
        {')', 3},
        {']', 57},
        {'}', 1197},
        {'>', 25137},
    };
}

Dictionary<char, int> BuildIncompleteScorer()
{
    return new Dictionary<char, int>()
    {
        {')', 1},
        {']', 2},
        {'}', 3},
        {'>', 4},
    };
}