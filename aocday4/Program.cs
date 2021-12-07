// See https://aka.ms/new-console-template for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");

var numbers = input[0].Split(',').Select(i => int.Parse(i)).ToArray();
List<int[][]> boards = new();

for (var i = 1; i < input.Length - 4; i++)
{
    if (input[i] != "")
    {
        var end = i + 5;
        var rows = input[i..end];


        var x = rows.Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray()).ToArray();
        
        boards.Add(x);
        i += 5;
    }
}

var called = new List<int>();
List<int[][]> winners = new();

foreach (var num in numbers)
{
    called.Add(num);
    
    foreach (var winner in FindWinner())
    {
        boards.Remove(winner);
        if (!winners.Contains(winner))
        {
            winners.Add(winner);
        }
    }

    if (winners.Count == 100)
    {
        var score = 0;
        var boardNums = winners.Last().SelectMany(r => r.Select(n => n)).ToArray();
        var notMarked = boardNums.Where(n => !called.Contains(n)).ToArray();

        score = notMarked.Sum();

        score *= num;
         
        Console.WriteLine(score);
    }
}


int[][][] FindWinner()
{
    var winner = boards.Where(b => HasRow(b) || HasColumn(b));
    return winner.ToArray();
}

bool HasRow(int[][] board)
{
    return board.Any(r => r.All(n => called!.Contains(n)));
}

bool HasColumn(int[][] board)
{
    for (int i = 0; i < 5; i++)
    {
        var column = new int[] {board[0][i], board[1][i], board[2][i], board[3][i], board[4][i]};

        if (column.All(c => called!.Contains(c)))
            return true;
    }

    return false;
}