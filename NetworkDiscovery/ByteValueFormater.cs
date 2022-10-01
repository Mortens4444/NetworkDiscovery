namespace NetworkDiscovery
{
    public static class ByteValueFormater
    {
        private static readonly char[] unitPrefixes = new[] { 'K', 'M', 'G', 'T', 'P', 'E', 'Z' };

        public static string ToHumanReadable(long sentBytesPerSecond, bool bitPerSec)
        {
            var baseUnit = bitPerSec ? "Bit" : "B";
            var currentUnit = baseUnit;

            foreach (var unitPrefix in unitPrefixes)
            {
                if (sentBytesPerSecond >= 1024)
                {
                    sentBytesPerSecond /= 1024;
                    currentUnit = $"{unitPrefix}{baseUnit}";
                }
                else
                {
                    break;
                }
            }

            return $"{sentBytesPerSecond} {currentUnit}/s";
        }
    }
}
