// Advent of Code 2021 - Day 16: Packet Decoder

using System.Collections;
using aocday16;

var input = File.ReadAllText("input.txt").Trim();
var rootPacket = ParseString(input);

var versionSum = CalculateVersionSum(rootPacket);
var answer2 = rootPacket.DoOperation();

Console.WriteLine(versionSum);
Console.WriteLine(answer2);

BitArray ConvertStringToBitArray(string str)
{
    var bits = new BitArray(str.Length * 4);
    for (int i = 0; i < str.Length; i++)
    {
        var c = str[i];
        var value = c is >= '0' and <= '9' ? c - '0' : c - 'A' + 10;
        bits[i * 4 + 0] = (value & 8) != 0;
        bits[i * 4 + 1] = (value & 4) != 0;
        bits[i * 4 + 2] = (value & 2) != 0;
        bits[i * 4 + 3] = (value & 1) != 0;
    }
    return bits;
}

long ParseBits(BitArray bits, int start, int end)
{
    long value = 0;
    
    for (var i = start; i <= end; i++)
        value = value * 2 + (bits[i] ? 1 : 0);
    
    return value;
}

(Packet, int) ConvertToPacket(BitArray bits, int start)
{
    var packet = new Packet()
    {
        Version = (int)ParseBits(bits, start, start + 2),
        Type = (int)ParseBits(bits, start + 3, start + 5)
    };
    
    // skip the preamble
    var i = start + 6;

    // Literal packet
    if (packet.Type == 4)
    {
        do
        {
            packet.LiteralValue *= 16L;
            packet.LiteralValue += ParseBits(bits, i + 1, i + 4);
            i += 5;
        } while (bits[i - 5]);

        return (packet, i);
    }

    // Operator packet
    if (!bits[i])
    {
        var length = ParseBits(bits, i + 1, i + 15);
        i += 16;
        var subPacketEnd = i + length;
        while (i < subPacketEnd)
        {
            var (subPacket, next) = ConvertToPacket(bits, i);
            packet.SubPackets.Add(subPacket);
            i = next;
        }
    }
    else
    {
        // Length is count of subpackets
        var length = ParseBits(bits, i + 1, i + 11);
        i += 12;
        while (packet.SubPackets.Count < length)
        {
            var (subPacket, next) = ConvertToPacket(bits, i);
            packet.SubPackets.Add(subPacket);
            i = next;
        }
    }
    return (packet, i);
}

Packet ParseString(string str)
{
    var stringBits = ConvertStringToBitArray(str);
    var (packet, _) = ConvertToPacket(stringBits, 0);
    return packet;
}

int CalculateVersionSum(Packet packet)
{
    var sum = packet.Version;
    if (packet.Type == 4) 
        return sum;
    
    return sum + packet.SubPackets.Sum(CalculateVersionSum);
}

