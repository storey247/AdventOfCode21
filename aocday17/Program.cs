// See https://aka.ms/new-console-template for more information

// test input
// var targetMinX = 20;
// var targetMaxX = 30;
// var targetMinY = -10;
// var targetMaxY = -5;

var targetMinX = 57;
var targetMaxX = 116;
var targetMinY = -198;
var targetMaxY = -148;

var maxYPosition = 0;
HashSet<(int, int)> unique = new();

for (int forward = 1; forward <= targetMaxX; forward++)
{
    for (int upward = -200; upward < 200; upward++) // these values -200..200 are just magic numbers i guessed at, theres probably a better way
    {
        var probe = (x: 0, y: 0, f: forward, u: upward);
        var probeHighestY = 0;
        
        while (HitPossible(probe.x,probe.y))
        {
            // adjust the probes location and velocity
            probe.x += probe.f;
            probe.y += probe.u;
            probe.f = probe.f > 0 ? probe.f - 1 : probe.f == 0 ? 0 : probe.f + 1;
            probe.u -= 1;
            
            // check if this is the highest y pos we've had
            probeHighestY = Math.Max(probeHighestY, probe.y);
            
            if (IsHit(probe.x, probe.y))
            {
                // if this was a hit, lets track its unique start point
                unique.Add((forward, upward));
                // update the variable that tracks the max Y position we got
                maxYPosition = Math.Max(probeHighestY, maxYPosition);
                break;
            }
        }
    }
}

Console.WriteLine(maxYPosition);
Console.WriteLine(unique.Count);

bool IsHit(int x, int y)
{
    // check if the current position is in the target
    return x >= targetMinX && x <= targetMaxX && y >= targetMinY && y <= targetMaxY;
}

bool HitPossible(int x, int y)
{
    // are we are still within the possibility of hitting the target
    return x <= targetMaxX && y >= targetMinY;
}