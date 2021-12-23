// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");

var imageEnhancementAlgorithm = input[0];

var imageMap = new HashSet<(int,int)>();

for (var line = 2; line < input.Length; line++)
{
    for (var character = 0; character < input[line].Length; character++)
    {
        if (input[line][character] == '#')
            imageMap.Add((line, character));
    }
}

int answer1 = 0;

for (var x = 0; x < 25; x++) // we do 2 passes in inner loop so half 50 == 25
for (var i = 0; i < 2; i++) // do 2 passes
{
    imageMap = EnhanceMap(imageMap, i == 0);

    if (x == 0)
        answer1 = imageMap.Count;
}

Console.WriteLine(answer1);
Console.WriteLine(imageMap.Count);

HashSet<(int,int)> EnhanceMap(HashSet<(int y,int x)> map, bool first)
{
    var points = GetPoints(map, 1);
    var enhancedImageMap = new HashSet<(int,int)>();

    for (var y = points.minY; y <= points.maxY; y++)
    for (var x = points.minX; x <= points.maxX; x++)
    {
        if (ShouldBitBeOn(y,x, map, first, points.minX, points.minY, points.maxX, points.maxY))
        {
            enhancedImageMap.Add((y,x));
        }
    }

    if (!first || imageEnhancementAlgorithm[0] != '#') 
        return enhancedImageMap;
    
    points = GetPoints(map, 2);

    for (var y = points.minY; y <= points.maxY; y++)
    {
        enhancedImageMap.Add((y, points.minX));
        enhancedImageMap.Add((y, points.maxX));
    }
    for (var x = points.minX; x <= points.maxX; x++)
    {
        enhancedImageMap.Add((points.minY, x));
        enhancedImageMap.Add((points.maxY, x));
    }
    return enhancedImageMap;
}

bool ShouldBitBeOn(int y, int x, HashSet<(int, int)> map, bool first, int minx, int miny, int maxx, int maxy)
{
    var index = 0;

    for (var dy = y - 1; dy <= y + 1; dy++)
    for (var dx = x - 1; dx <= x + 1; dx++)
    {
        index *= 2;
        if (map.Contains((dy,dx)) || (!first && imageEnhancementAlgorithm[0] == '#' 
                                             && (dy <= miny || dy >= maxy || dx <= minx || dx >= maxx)))
        {
            index++;
        }
    }

    return imageEnhancementAlgorithm[index] == '#';
}

(int minX, int minY, int maxX, int maxY) GetPoints(HashSet<(int, int)> map, int offset)
{
    return (map.MinBy(pos => pos.Item2).Item2 - offset,
        map.MinBy(pos => pos.Item1).Item1 - offset,
        map.MaxBy(pos => pos.Item2).Item2 + offset,
        map.MaxBy(pos => pos.Item1).Item1 + offset);
}