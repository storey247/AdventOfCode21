// See https://aka.ms/new-console-template for more information

using aocextensions;

var paths = File.ReadAllLines("input.txt")
    .Select(i => i.Split("-"))
    .SelectMany(x => new[] { (x[0], x[1]), (x[1], x[0]) })
    .ToLookup(x => x.Item1, x => x.Item2);

// part 1
var answer = CountPaths(paths, "start", "end", new Dictionary<string, int> { {"start", 1} });
Console.WriteLine(answer);

// part 2
var answer2 = CountPaths(paths, "start", "end", new Dictionary<string, int> { {"start", 1} }, true);
Console.WriteLine(answer2);

if (answer != 3230)
    throw new Exception();

if (answer2 != 83475)
    throw new Exception();

int CountPaths(ILookup<string, string> validPaths, string src, string dest, Dictionary<string, int> visitedSmallCaves, bool allowDoubleVisits = false)
{
    if (src == dest)
        return 1;

    var ans = 0;
    foreach (var neighbor in validPaths[src])
    {
        var alreadyVisited = visitedSmallCaves.ContainsKey(neighbor);
        // we've already seen this small cave and we don't allow double visits, skip this one
        if (!allowDoubleVisits && alreadyVisited)
            continue;

        // we've already seen this small cave and we are allowing double visits
        if (allowDoubleVisits && alreadyVisited)
        {
            // because we only store small caves, and we have already checked the dictionary contains this cave...
            // we can only visit one small cave 2 times, so if theres already a 2... bail out!
            if (IsInvalidCave(neighbor) || visitedSmallCaves.ContainsValue(2))
                continue;
        }
        
        var newVisited = new Dictionary<string, int>(visitedSmallCaves);
        if (IsSmallCave(neighbor))
            newVisited[neighbor] = visitedSmallCaves.ContainsKey(neighbor) ? 2 : 1;
        
        // use dreaded recursion... FMAL I blame Mike Reeves for this!
        ans += CountPaths(validPaths, neighbor, dest, newVisited, allowDoubleVisits);
    }

    return ans;
}

bool IsInvalidCave(string cave)
    => cave is "start" or "end";

bool IsSmallCave(string cave)
    => cave.IsAllLower();