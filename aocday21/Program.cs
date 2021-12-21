// See https://aka.ms/new-console-template for more information

var player1Position = 4;
var player2Position = 5; // 5

var rolls = 0;
var dice = 1;

var player1Score = 0;
var player2Score = 0;

bool player1 = true;

while (player1Score < 1000 && player2Score < 1000)
{
    var turn = 0;
    for (int i = 0; i < 3; i++)
    {
        rolls++;
        turn += dice++;
        if (dice > 100)
            dice = 1;
    }

    if (player1)
    {
        player1Position = CalcPlayerPosition(player1Position, turn);
        player1Score += player1Position;

    }
    else
    {
        player2Position = CalcPlayerPosition(player2Position, turn);
        player2Score += player2Position;
    }


    player1 = !player1;
}

var answer1 = Math.Min(player1Score, player2Score) * rolls;

Console.WriteLine(answer1);


// part 2 with quantum dice!

player1Position = 4;
player2Position = 5;
long player1Wins = 0;
long player2Wins = 0;

Dictionary<(int P1, int P2, int S1, int S2, bool Turn, int Toss), (long, long)> winCache = new();

for (int i = 1; i <= 3; i++)
{
    for (int j = 1; j <= 3; j++)
    {
        for (int k = 1; k <= 3; k++)
        {
            (long p1WinCount, long p2WinCount) = QuantumRoll(player1Position, 0, player2Position, 0, false, i + j + k);
            player1Wins += p1WinCount;
            player2Wins += p2WinCount;
        }
    }
}

var answer2 = Math.Max(player2Wins, player1Wins);
Console.WriteLine(answer2);

if (answer2 != 575111835924670)
    throw new Exception();

int CalcPlayerPosition(int playerPosition, int turn)
{
    var position = playerPosition + turn;

    while (position > 10)
        position -= 10;

    return position;
}

(long, long) QuantumRoll(int player1Position, int player1Score, int player2Position, int player2Score, bool player2Turn, int toss)
{
    if (winCache.TryGetValue((player1Position, player2Position, player1Score, player2Score, player2Turn, toss), out var winner))
        return winner;

    int pos = !player2Turn ? player1Position : player2Position;
    int score = !player2Turn ? player1Score : player2Score;

    pos = CalcPlayerPosition(pos, toss);
    score += pos;

    if (score >= 21)
    {
        // a player won
        var result = player2Turn ? (0, 1) : (1, 0);
        winCache[(player1Position, player2Position, player1Score, player2Score, player2Turn, toss)] = result;
        return result;
    }


    // we need to recurse and do another set of rolls
    (int p1Position, int p2Position, int p1Score, int p2Score) = (player1Position, player2Position, player1Score, player2Score);

    if (!player2Turn)
    {
        player1Position = pos;
        player1Score = score;
    }
    else
    {
        player2Position = pos;
        player2Score = score;
    }

    long player1Wins = 0;
    long player2Wins = 0;

    for (int i = 1; i <= 3; i++)
    {
        for (int j = 1; j <= 3; j++)
        {
            for (int k = 1; k <= 3; k++)
            {
                var result = QuantumRoll(player1Position, player1Score, player2Position, player2Score, !player2Turn, i + j + k);
                player1Wins += result.Item1;
                player2Wins += result.Item2;
            }
        }
    }
    winCache[(p1Position, p2Position, p1Score, p2Score, player2Turn, toss)] = (player1Wins, player2Wins);
    return (player1Wins, player2Wins);

}