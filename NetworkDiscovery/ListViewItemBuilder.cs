using System.Net;
using System.Net.NetworkInformation;

namespace NetworkDiscovery;

public static class ListViewItemBuilder
{
    public static ListViewItem? Construct(string ipAddressStr)
    {
        var ipAddress = IPAddress.Parse(ipAddressStr);
        var phyhicalAddress = MacAddressProvider.GetByIpAddress(ipAddress);
        if (phyhicalAddress != null)
        {
            var result = new ListViewItem(ipAddressStr);
            result.SubItems.Add(HostNameProvider.GetByIpAddress(ipAddressStr));
            result.SubItems.Add(MacAddressToStringConverter.Convert(phyhicalAddress));

            if (LocalIpChecker.IsLocal(ipAddressStr))
            {
                var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(nic => nic.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(ipInfo => ipInfo.Address.ToString() == ipAddressStr) != null);

                if (networkInterface != null)
                {
                    result.SubItems.Add(networkInterface.Name);
                    result.SubItems.Add(networkInterface.NetworkInterfaceType.ToString());
                    result.SubItems.Add(networkInterface.Description);
                    result.SubItems.Add(ByteValueFormatter.ToHumanReadable(networkInterface.Speed, true));

                    var stats = networkInterface.GetIPv4Statistics();
                    var previousSentBytes = stats.BytesSent;
                    var previousReceivedBytes = stats.BytesReceived;
                    Thread.Sleep(1000);
                    stats = networkInterface.GetIPv4Statistics();

                    result.SubItems.Add(ByteValueFormatter.ToHumanReadable(stats.BytesSent - previousSentBytes, false));
                    result.SubItems.Add(ByteValueFormatter.ToHumanReadable(stats.BytesReceived - previousReceivedBytes, false));
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        result.SubItems.Add("N/A");
                    }
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    result.SubItems.Add(String.Empty);
                }
            }

            return result;
        }
        return null;
    }
}
