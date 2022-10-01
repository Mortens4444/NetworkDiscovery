using System;
using System.Net;

namespace NetworkDiscovery
{
    public static class HostNameProvider
    {
        public static string GetByIpAddress(string ip)
        {
            string result = string.Empty;
            try
            {
                IPHostEntry hostInfo;
                try
                {
                    if (Environment.OSVersion.Version.Major > 5) // From Windows Vista
                    {
                        hostInfo = Dns.GetHostEntry(ip);
                    }
                    else
                    {
                        hostInfo = Dns.GetHostByAddress(ip);
                    }
                }
                catch
                {
                    hostInfo = Dns.GetHostEntry(ip);
                }
                result = hostInfo.HostName;
            }
            catch { }
            return result;
        }
    }
}
