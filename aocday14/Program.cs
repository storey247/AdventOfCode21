// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");
var polymer = input[0];

var instructions = input[2..]
    .Select(i => i.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
    .ToDictionary(x => x[0], x=> x[1]);

Dictionary<string, long> polymerCounter = new();

// seed the polymer counter with the starting input
for (int i = 0; i < polymer.Length - 1; i++)
{
    var pair = polymer.Substring(i, 2);
    polymerCounter[pair] = polymerCounter.GetValueOrDefault(pair, 0) + 1;
}

for (int i = 0; i < 40; i++)
{
    Dictionary<string, long> newPolymerCounter = new();
    foreach(var pairing in polymerCounter)
    {
        var pair = pairing.Key;
        var newPolymerPart = instructions[pair];
        var left = pair[0] + newPolymerPart;
        var right = newPolymerPart + pair[1];

        newPolymerCounter[left] = newPolymerCounter.GetValueOrDefault(left,0) + pairing.Value;
        newPolymerCounter[right] = newPolymerCounter.GetValueOrDefault(right, 0) + pairing.Value;
    }

    // replace the old dictionary with the new one
    polymerCounter = newPolymerCounter;
}

Dictionary<char, long> charCounter = new();
foreach (var paring in polymerCounter)
{
    charCounter[paring.Key[0]] = charCounter.GetValueOrDefault(paring.Key[0], 0) + paring.Value;
}

// need to remember to not ignore the very last part of the original polymer :/ 
charCounter[polymer[^1]] = charCounter.GetValueOrDefault(polymer[^1], 0) + 1;

var max = charCounter.Values.Max();
var min = charCounter.Values.Min();
var answer = max - min;

Console.WriteLine(answer);

if (answer != 2942885922173)
    throw new Exception();