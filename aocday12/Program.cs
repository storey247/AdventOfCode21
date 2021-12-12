// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");
var pathParts = input.Select(i => i.Split("-"))
    .SelectMany(x => new[] { (x[0], x[1]), (x[1], x[0]) });

var paths = pathParts.ToLookup(x => x.Item1, x => x.Item2);

// part 1
var answer = CountPaths(paths, "start", "end", new HashSet<string> { "start" }).ToString();
Console.WriteLine(answer);


int CountPaths(ILookup<string, string> graph, string src, string dest, HashSet<string> seen, bool allowDoubleVisits = false)
{
    if (src.Equals(dest))
    {
        return 1;
    }

    var ans = 0;
    foreach(var neighbor in graph[src].Where(n => !seen.Contains(n)))
    {
        var newSeen = new HashSet<string>(seen);
        if (neighbor.All(char.IsLower)) newSeen.Add(neighbor);
        ans += CountPaths(graph, neighbor, dest, newSeen);
    }
    return ans;
}