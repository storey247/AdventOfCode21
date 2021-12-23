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
    var minx = map.MinBy(pos => pos.x).x - 1;
    var miny = map.MinBy(pos => pos.y).y - 1;
    var maxx = map.MaxBy(pos => pos.x).x + 1;
    var maxy = map.MaxBy(pos => pos.y).y + 1;

    var enhancedImageMap = new HashSet<(int,int)>();

    for (var y = miny; y <= maxy; y++)
    for (var x = minx; x <= maxx; x++)
    {
        if (ShouldBitBeOn(y,x, map, first, minx, miny, maxx, maxy))
        {
            enhancedImageMap.Add((y,x));
        }
    }

    if (!first || imageEnhancementAlgorithm[0] != '#') 
        return enhancedImageMap;
    
    minx = map.MinBy(pos => pos.x).x - 2;
    miny = map.MinBy(pos => pos.y).y - 2;
    maxx = map.MaxBy(pos => pos.x).x + 2;
    maxy = map.MaxBy(pos => pos.y).y + 2;

    for (var y = miny; y <= maxy; y++)
    {
        enhancedImageMap.Add((y,minx));
        enhancedImageMap.Add((y,maxx));
    }
    for (var x = minx; x <= maxx; x++)
    {
        enhancedImageMap.Add((miny,x));
        enhancedImageMap.Add((maxy,x));
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
