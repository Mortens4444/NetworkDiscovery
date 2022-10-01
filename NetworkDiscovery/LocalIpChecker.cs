using System.Net;

namespace NetworkDiscovery
{
    public static class LocalIpChecker
    {
        public static bool IsLocal(string host)
        {
            try
            {
                var hostIPs = Dns.GetHostAddresses(host);
                var localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                foreach (var hostIP in hostIPs)
                {
                    if (IPAddress.IsLoopback(hostIP))
                    {
                        return true;
                    }
                    foreach (var localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP))
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
