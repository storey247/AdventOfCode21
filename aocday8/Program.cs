// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");
var entries = input.Select(i => i.Split("|", StringSplitOptions.TrimEntries)).ToArray();
var answer = 0;

foreach (var value in entries)
{
    var decoder = BuildDecoder();
    var leftPart = value[0].Split();
    var rightPart = value[1].Split();

    decoder[8] = leftPart.First(Is8).ToList();
    decoder[7] = leftPart.First(Is7).ToList();
    decoder[1] = leftPart.First(Is1).ToList();
    decoder[4] = leftPart.First(Is4).ToList();

    decoder[6] = leftPart.First(x => x.Length == 6 && !decoder[7].All(a => x.Contains(a)) && !decoder.ContainsValue(x.ToList())).ToList();

    char c = decoder[1].Except(decoder[6]).First();
    char f = decoder[1].Except(new char[] { c }).First();
    

    decoder[3] = leftPart.First(x => x.Length == 5 && (x.Contains(c) && x.Contains(f)) && !decoder.ContainsValue(x.ToList())).ToList();
    decoder[2] = leftPart.First(x => x.Length == 5 && (x.Contains(c) && !x.Contains(f)) && !decoder.ContainsValue(x.ToList())).ToList();
    decoder[5] = leftPart.First(x => x.Length == 5 && (!x.Contains(c) && x.Contains(f)) && !decoder.ContainsValue(x.ToList())).ToList();
    
    char e = decoder[6].Except(decoder[5]).First();

    decoder[0] = leftPart.First(x => x.Length == 6 && x.Contains(e) && !decoder.ContainsValue(x.ToList())).ToList();
    decoder[9] = leftPart.First(x => !decoder.ContainsValue(x.ToList())).ToList();

    var reverseDecoder = new Dictionary<string, int>();
    foreach (var entry in decoder)
    {
        reverseDecoder.Add(new string(entry.Value.ToArray()), entry.Key);
    }
    
    var solution = "";
    foreach (var o in rightPart)
        solution += reverseDecoder[o];

    answer += int.Parse(solution);
}

Console.WriteLine(answer);

bool Is8(string val) => val.Length == 7;
bool Is7(string val) => val.Length == 3;
bool Is1(string val) => val.Length == 2;
bool Is4(string val) => val.Length == 4;


Dictionary<int, List<char>> BuildDecoder() => new();