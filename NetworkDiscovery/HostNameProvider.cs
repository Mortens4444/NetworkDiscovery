using System.Diagnostics;
using System.Net;

namespace NetworkDiscovery;

public static class HostNameProvider
{
    public static string GetByIpAddress(string ip)
    {
        string result = string.Empty;
        IPHostEntry hostInfo;
        try
        {
            hostInfo = Dns.GetHostEntry(ip);
            result = hostInfo.HostName;
        }
#if DEBUG
        catch (Exception ex)
        {
            Debug.WriteLine($"HostNameProvider error: {ex.Message}");
        }
#else
        catch { }
#endif
        return result;
    }
}
