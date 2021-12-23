// See https://aka.ms/new-console-template for more information

// var targetMinX = 20;
// var targetMaxX = 30;
// var targetMinY = -10;
// var targetMaxY = -5;

var targetMinX = 57;
var targetMaxX = 116;
var targetMinY = 198;
var targetMaxY = -148;

var maxYPosition = 0;

for (int forward = 1; forward < targetMinX; forward++) // a forward of 0 wont work
{
    for (int upward = 0; upward < 100; upward++)
    {
        var probe = (x: 0, y: 0, f: forward, u: upward);
        var probeHighestY = 0;
        
        while (HitPossible(probe.x,probe.y))
        {
            probe.x += probe.f;
            probe.y += probe.u;
            probe.f = probe.f > 0 ? probe.f - 1 : probe.f == 0 ? 0 : probe.f + 1;
            probe.u -= 1;
            
            probeHighestY = Math.Max(probeHighestY, probe.y);
            
            if (IsHit(probe.x, probe.y))
            {
                maxYPosition = Math.Max(probeHighestY, maxYPosition);
                break;
            }
        }
    }
}

Console.WriteLine(maxYPosition);

bool IsHit(int x, int y)
{
    return (x >= targetMinX && x <= targetMaxX) 
           && (y >= targetMinY && y <= targetMaxY);
}

bool HitPossible(int x, int y)
{
    return x <= targetMaxX && y >= targetMaxY;
}