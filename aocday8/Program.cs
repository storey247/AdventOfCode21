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
    Dictionary<int, List<char>> decoder = new();

    // char enumerables will allow us easy searching for the necessary parts
    List<List<char>> inputs = new();
    foreach(var x in value[0].Split()[..10])
    {
        var tmp = x.ToList();
        inputs.Add(tmp);
    }
    
    List<string> outputs = new();
    foreach (var x in value[1].Split()[..])
    {
        var tmp = x.ToList();
        tmp.Sort();
        outputs.Add(new(tmp.ToArray()));
    }
    
    // these are the easy ones to get
    decoder[1] = inputs.First(x => x.Count == 2);
    decoder[4] = inputs.First(x => x.Count == 4);
    decoder[7] = inputs.First(x => x.Count == 3);
    decoder[8] = inputs.First(x => x.Count == 7);

    // 6 must have length 6 and contain all elements of 7
    decoder[6] = inputs.First(x => x.Count == 6 && !decoder[7].All(x.Contains) && !decoder.ContainsValue(x));

    // now we have 6 we can work out what the c and f signals are
    var c = decoder[1].Except(decoder[6]).First();
    var f = decoder[1].Except(new[] { c }).First();

    // now we have c and f by process of elimination we can find 3, 2 and 5
    decoder[3] = inputs.First(x => x.Count == 5 && (x.Contains(c) && x.Contains(f)) && !decoder.ContainsValue(x));
    decoder[2] = inputs.First(x => x.Count == 5 && (x.Contains(c) && !x.Contains(f)) && !decoder.ContainsValue(x));
    decoder[5] = inputs.First(x => x.Count == 5 && (!x.Contains(c) && x.Contains(f)) && !decoder.ContainsValue(x));

    // signal for e must be the elements of 6 not in 5
    var e = decoder[6].Except(decoder[5]).First();

    // now we can just figure out the last 2
    decoder[0] = inputs.First(x => x.Count == 6 && x.Contains(e) && !decoder.ContainsValue(x));
    decoder[9] = inputs.First(x => !decoder.ContainsValue(x));
    
    // now we just reverse lookup into decoder and get our string representation of hte output
    var solution = "";
    foreach (var o in outputs)
       solution += decoder.FirstOrDefault(x =>
       {
           x.Value.Sort();
           var converted = new string(x.Value.ToArray());
           return converted == o;
       }).Key;

    // total up the answer
    answer += int.Parse(solution);
}

Console.WriteLine(answer);
