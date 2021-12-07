// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");

CalculateLifeSupport();


void CalculateLifeSupport()
{
    var oxygen = input;
    var scrubber = input;
    var oxy = "";
    var co2 = "";

    for (int i = 0; i < input[0].Length; i++)
    {
        if (oxygen.Length > 1)
        {
            var frequent = MostFrequent(oxygen, i);
            oxy += frequent;
            oxygen = oxygen.Where(o => o[i] == frequent).ToArray();
        }

        if (scrubber.Length > 1)
        {
            var frequent = MostFrequent(scrubber, i);
            co2 += frequent == '1' ? '0' : '1';
            scrubber = scrubber.Where(o => o[i] != frequent).ToArray();
        }
    }
    
    var rating = Convert.ToInt32(oxygen.Single(), 2) * Convert.ToInt32(scrubber.Single(), 2);
    
    Console.WriteLine(rating);

}


char MostFrequent(string[] input, int position)
{
    var zeros = input.Count(i => i[position] == '0');
    var ones = input.Count(i => i[position] == '1');

    if (zeros == ones)
        return '1';
    
    return zeros > ones ? '0' : '1';
}

void CalculatePower()
{
    var gamma = "";
    var epsilon = "";

    for (var i = 0; i < input[0].Length; i++)
    {
        var ones = 0;
        var zeros = 0;

        foreach (var row in input)
        {
            if (row[i] == '0')
                zeros++;
            else
                ones++;
        }

        gamma += ones > zeros ? "1" : "0";
        epsilon += ones > zeros ? "0" : "1";
    }

    var power = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);

    Console.WriteLine(power);
}