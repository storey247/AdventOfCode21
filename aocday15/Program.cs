// See https://aka.ms/new-console-template for more information

using System.Drawing;
using aocextensions;

var input = InputExtensions.GetAllInputAsInt("input.txt");

var map = BuildInputMap();
var biggerMap = BuildInputMap(5);

var bottomRight = new Point(map.Keys.Max(p => p.X), map.Keys.Max(p => p.Y));
var riskMap = CalculateRouteRiskMap(map);
var answer1 = riskMap[bottomRight];

bottomRight = new Point(biggerMap.Keys.Max(p => p.X), biggerMap.Keys.Max(p => p.Y));
var riskMap2 = CalculateRouteRiskMap(biggerMap);
var answer2 = riskMap2[bottomRight];

Console.WriteLine(answer1);
Console.WriteLine(answer2);

Dictionary<Point, int> CalculateRouteRiskMap(Dictionary<Point, int> inputMap) {

    var topLeft = new Point(0, 0);
    
    PriorityQueue<Point, int> pointsToProcess = new();
    Dictionary<Point, int> totalRiskMap = new()
    {
        [topLeft] = 0
    };

    pointsToProcess.Enqueue(topLeft, 0);

    // Using the Disjktra algorithm here
    while (pointsToProcess.Count > 0) {
        var pointToProcess = pointsToProcess.Dequeue();

        foreach (var neighbour in pointToProcess.GetNeighbours())
        {
            // only process this neighbour if we haven't already or if its a point in the map
            if (!inputMap.ContainsKey(neighbour) || totalRiskMap.ContainsKey(neighbour)) 
                continue;
            
            // add together the current risk and the risk of the neighbours for this point
            var totalRisk = totalRiskMap[pointToProcess] + inputMap[neighbour];
            totalRiskMap[neighbour] = totalRisk;
            
            // we need to process this neighbour next
            pointsToProcess.Enqueue(neighbour, totalRisk);
        }
    }

    return totalRiskMap;
}

Dictionary<Point, int> BuildInputMap(int scaleUp = 0)
{
    Dictionary<Point, int> buildMap = new();

    for (var row = 0; row < input.Length; row++)
    {
        for (var col = 0; col < input[0].Length; col++)
        {
            buildMap.Add(new Point(row, col), input[row][col]);
        }
    }

    if (scaleUp <= 0) 
        return buildMap;
    
    Dictionary<Point, int> scaledMap = new();

    for (int y = 0; y < scaleUp; y++)
    {
        var increaseY = (buildMap.Max(p => p.Key.Y) +1) * y;
            
        for (int x = 0; x < scaleUp; x++)
        {
            var increaseX = (buildMap.Max(p => p.Key.X) +1) * x;

            foreach (var (key, value) in buildMap)
            {
                var newX = key.X + increaseX;
                var newY = key.Y + increaseY;
                    
                var newRisk = value + x + y;

                if (newRisk > 9)
                    newRisk %= 9;
                    
                scaledMap.Add(new Point(newX, newY), newRisk);
            }
        }
    }

    return scaledMap;
}