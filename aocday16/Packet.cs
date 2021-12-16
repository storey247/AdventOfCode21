namespace aocday16;

internal class Packet
{
    public int Version;
    public int Type;
    public long LiteralValue;
    public readonly List<Packet> SubPackets = new();
    
    public long DoOperation()
    {
        return Type switch
        {
            4 => LiteralValue,
            0 => SubPackets.Sum(x => x.DoOperation()),
            1 => SubPackets.Aggregate(1L, (acc, sub) => acc * sub.DoOperation()),
            2 => SubPackets.Min(x => x.DoOperation()),
            3 => SubPackets.Max(x => x.DoOperation()),
            5 => SubPackets[0].DoOperation() > SubPackets[1].DoOperation() ? 1 : 0,
            6 => SubPackets[0].DoOperation() < SubPackets[1].DoOperation() ? 1 : 0,
            7 => SubPackets[0].DoOperation() == SubPackets[1].DoOperation() ? 1 : 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}