using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace NetworkDiscovery
{
    public static class ListViewItemBuilder
    {
        public static void AddItemIfAvailable(string ipAddressStr, ListView lvDevices)
        {
            var items = new ArrayList();

            ListViewItem item;
            item = new ListViewItem(ipAddressStr);
            item.SubItems.Add(HostNameProvider.GetByIpAddress(ipAddressStr));
            var ipAddress = IPAddress.Parse(ipAddressStr);
            var phyhicalAddress = MacAddressProvider.GetByIpAddress(ipAddress);
            if (phyhicalAddress != null)
            {
                item.SubItems.Add(MacAddressToStringConverter.Convert(phyhicalAddress));

                if (LocalIpChecker.IsLocal(ipAddressStr))
                {
                    var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                        .FirstOrDefault(nic => nic.GetIPProperties().UnicastAddresses
                            .FirstOrDefault(ipInfo => ipInfo.Address.ToString() == ipAddressStr) != null);

                    if (networkInterface != null)
                    {
                        item.SubItems.Add(networkInterface.Name);
                        item.SubItems.Add(networkInterface.NetworkInterfaceType.ToString());
                        item.SubItems.Add(networkInterface.Description);
                        item.SubItems.Add(ByteValueFormater.ToHumanReadable(networkInterface.Speed, true));

                        var stats = networkInterface.GetIPv4Statistics();
                        var previousSentBytes = stats.BytesSent;
                        var previousReceivedBytes = stats.BytesReceived;
                        Thread.Sleep(1000);
                        stats = networkInterface.GetIPv4Statistics();

                        item.SubItems.Add(ByteValueFormater.ToHumanReadable(stats.BytesSent - previousSentBytes, false));
                        item.SubItems.Add(ByteValueFormater.ToHumanReadable(stats.BytesReceived - previousReceivedBytes, false));
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            item.SubItems.Add("N/A");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        item.SubItems.Add(String.Empty);
                    }
                }

                ListViewItemAppender.AddToList(lvDevices, item);
            }
        }
    }
}
