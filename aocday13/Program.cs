// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");

var coordinates = new List<Tuple<int, int>>();
var folds = new List<string>();

foreach (var line in input)
{
    if (line.Contains(','))
    {
        var splitLine = line.Split(',').Select(int.Parse).ToArray();
        coordinates.Add(new (splitLine[0], splitLine[1]));
    }
    else if (line.Contains('='))
    {
        folds.Add(line);
    }
}

var maxRows = coordinates.Max(x => x.Item2);
var maxCols = coordinates.Max(x => x.Item1);

char[,] paper = new char[maxRows +1, maxCols +1];

foreach (var line in coordinates)
{
    paper[line.Item2, line.Item1] = '#';
}

var answer1 = 0;

for (int i = 0; i < folds.Count; i++)
{
    var fold = folds[i][11..];
    if (fold[0] == 'y')
        paper = DoVerticalFold(paper, int.Parse(fold.Split('=')[1]));
    else
        paper = DoHorizontalFold(paper, int.Parse(fold.Split('=')[1]));

    if (i == 0)
        answer1 = GetHashCount(paper);
}

for (int i = 0; i < paper.GetLength(0); i++)
{
    for (int j = 0; j < paper.GetLength(1); j++)
    {
        Console.Write(paper[i,j] == '#' ? '#' : ' ');
    }
    
    Console.WriteLine();
}

Console.WriteLine(answer1);

char[,] DoVerticalFold(char[,] inputPaper, int row)
{
    var paperLength = inputPaper.GetLength(0) - 1;

    char[,] newPaper = new char[row, inputPaper.GetLength(1)];
    
    for (int i = row -1; i >= 0; i--)
    {
        for (int x = 0; x < inputPaper.GetLength(1); x++)
        {
            newPaper[i, x] = inputPaper[i, x]; // everything above fold gets copied
            if (row + (row -i) <= paperLength)
                newPaper[i, x] = inputPaper[i, x] == '#' ? '#' : inputPaper[row + (row -i), x]; // copy from below fold
        }
    }

    return newPaper;
}

char[,] DoHorizontalFold(char[,] inputPaper, int col)
{
    var paperWidth = inputPaper.GetLength(1) - 1;
    char[,] newPaper = new char[inputPaper.GetLength(0), col];

    for (int row = 0; row < inputPaper.GetLength(0); row++)
    {
        for (int x = 0; x < col; x++)
        {
            newPaper[row, x] = inputPaper[row, x];
            newPaper[row, x] = newPaper[row, x] == '#' ? '#' : inputPaper[row, paperWidth-x]; // copy from right of fold
        }
    }
    
    return newPaper;
}

int GetHashCount(char[,] inputPaper)
{
    var count = 0;
    foreach (var c in inputPaper)
        count += c == '#' ? 1 : 0;
    
    return count;
}