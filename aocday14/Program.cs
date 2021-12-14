// See https://aka.ms/new-console-template for more information

using System.Text;

var input = File.ReadAllLines("input.txt");

var polymer = new StringBuilder(input[0]);

var instructions = input[2..]
    .Select(i => i.Split("->", StringSplitOptions.RemoveEmptyEntries))
    .ToDictionary(x => x[0].Trim(), x=> x[1].Trim());

for (int i = 0; i < 40; i++)
{
    var chunks = Chunk(polymer.ToString(), 2);
    var cluster = 1;
    
    foreach (var chunk in chunks)
    {
        var newPolymer = instructions[chunk];
        polymer.Insert(cluster, newPolymer);
        cluster += 2;
    }
    
    Console.WriteLine(i);
}

var ordered = polymer.ToString()
    .GroupBy(x => x)
    .Select(group => new { group.Key, Count = group.Count() })
    .OrderBy(x => x.Count);

var least = ordered.First().Count;
var most = ordered.Last().Count;

var answer1 = most - least;

Console.WriteLine(answer1);


static IEnumerable<string> Chunk(string str, int chunkSize)
{
    List<string> chunks = new();
    
    for (int i = 0; i < str.Length - 1; i++)
    {
        var endIndex = i + chunkSize;
        chunks.Add(str[i..endIndex]);
    }

    return chunks;
}