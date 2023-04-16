namespace NetworkDiscovery;

public static class ByteValueFormatter
{
    private static readonly char[] unitPrefixes = new[] { 'K', 'M', 'G', 'T', 'P', 'E', 'Z' };

    public static string ToHumanReadable(long value, bool bitPerSec)
    {
        var baseUnit = bitPerSec ? "Bit" : "B";
        var currentUnit = baseUnit;

        foreach (var unitPrefix in unitPrefixes)
        {
            if (value >= 1024)
            {
                value /= 1024;
                currentUnit = $"{unitPrefix}{baseUnit}";
            }
            else
            {
                break;
            }
        }

        return $"{value} {currentUnit}/s";
    }
}
