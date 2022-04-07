using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace NetworkDiscovery
{
	public partial class MainForm : Form
	{
		private const int NO_ERROR = 0;
		private const int ERROR_BAD_NET_NAME = 67;
		private const int ERROR_BUFFER_OVERFLOW = 111;
		private const int ERROR_GEN_FAILURE = 31;
		private const int ERROR_INVALID_PARAMETER = 87;
		private const int ERROR_NOT_SUPPORTED = 50;

		[DllImport("iphlpapi.dll", ExactSpelling = true)]
		public static extern int SendARP(uint DestIP, uint SrcIP, byte[] pMacAddr, ref int PhyAddrLen);

		public MainForm()
		{
			InitializeComponent();

			Discovery();
		}

		private void Discovery()
		{
			var hostname = Dns.GetHostName();
			var ips = Dns.GetHostEntry(hostname);

			foreach (var ip in ips.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetworkV6)
				{
					DiscoverHost(ip.ToString());
				}
				else
				{
					var subnet = ip.ToString().Substring(0, ip.ToString().LastIndexOf('.'));

					var trds = new Thread[255];
					for (int i = 1; i < 255; i++)
					{
						trds[i] = new Thread(DiscoverHost)
						{
							Name = $"{subnet}.{i}"
						};
						trds[i].Start(trds[i].Name);
					}
				}
			}
		}

		private void DiscoverHost(object ipAddressObj)
		{
			string ipAddressStr = ipAddressObj.ToString();
			var items = new ArrayList();

			try
			{
				ListViewItem item;
				item = new ListViewItem(ipAddressStr);
				item.SubItems.Add(GetHostName(ipAddressStr));
				var ipAddress = IPAddress.Parse(ipAddressStr);
				var phyhicalAddress = GetMACFromNetworkComputer(ipAddress);
				item.SubItems.Add(MacAddressToString(phyhicalAddress));

				if (IsLocalIPAddress(ipAddressStr))
				{
					var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
						.FirstOrDefault(nic => nic.GetIPProperties().UnicastAddresses
							.FirstOrDefault(ipInfo => ipInfo.Address.ToString() == ipAddressStr) != null);

					if (networkInterface != null)
					{
						item.SubItems.Add(networkInterface.Name);
						item.SubItems.Add(networkInterface.NetworkInterfaceType.ToString());
						item.SubItems.Add(networkInterface.Description);
						item.SubItems.Add(FormatValue(networkInterface.Speed, true));

						var stats = networkInterface.GetIPv4Statistics();
						var previousSentBytes = stats.BytesSent;
						var previousReceivedBytes = stats.BytesReceived;
						Thread.Sleep(1000);
						stats = networkInterface.GetIPv4Statistics();

						item.SubItems.Add(FormatValue(stats.BytesSent, previousSentBytes));
						item.SubItems.Add(FormatValue(stats.BytesReceived, previousReceivedBytes));
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

				AddItemToList(lvDevices, item);
				Update();
			}
			catch { }
		}

		private static string FormatValue(long allSentBytes1, long allSentBytes2)
		{
			var sentBytesPerSecond = allSentBytes1 - allSentBytes2;
			return FormatValue(sentBytesPerSecond, false);
		}

		private static string FormatValue(long sentBytesPerSecond, bool bitPerSec)
		{
			var unit = bitPerSec ? "Bit" : "B";
			if (ChangeValue(ref sentBytesPerSecond, ref unit, bitPerSec ? "KBit" : "KB"))
			{
				if (ChangeValue(ref sentBytesPerSecond, ref unit, bitPerSec ? "MBit" : "MB"))
				{
					ChangeValue(ref sentBytesPerSecond, ref unit, bitPerSec ? "GBit" : "GB");
				}
			}
			return $"{sentBytesPerSecond} {unit}/s";
		}

		private static bool ChangeValue(ref long value, ref string unit, string newUnit)
		{
			if (value > 1024)
			{
				value = value / 1024;
				unit = newUnit;
				return true;
			}
			return false;
		}

		delegate void voidResultListViewListViewItemParams(ListView listView, ListViewItem listViewItem);
		private void AddItemToList(ListView listView, ListViewItem listViewItem)
		{
			try
			{
				if (!listView.InvokeRequired)
				{
					var backColor = listView.Items.Count % 2 == 0 ? listView.BackColor : Color.LightBlue;
					listViewItem.BackColor = backColor;
					listView.Items.Add(listViewItem);
				}
				else
				{
					var addItemToListDelegate = new voidResultListViewListViewItemParams(AddItemToList);
					Invoke(addItemToListDelegate, listView, listViewItem);
				}
			}
			catch {}
		}

		private static string GetHostName(string ip)
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

		private static bool IsLocalIPAddress(string host)
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
					foreach(var localIP in localIPs)
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

		private static PhysicalAddress GetMACFromNetworkComputer(IPAddress ipAddress)
		{
			PhysicalAddress result = null;
			try
			{

				if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					var ipv6Address = ipAddress.ToString();
					var percentIndex = ipv6Address.LastIndexOf("%");
					if (percentIndex != -1)
					{
						ipv6Address = ipv6Address.Substring(0, percentIndex);
					}
					var process = Process.Start(new ProcessStartInfo
					{
						FileName = "cmd",
						Arguments = $"/c netsh int ipv6 show neigh | findstr {ipv6Address}",
						UseShellExecute = false,
						RedirectStandardOutput = true
					});
					process.WaitForExit();
					var output = process.StandardOutput.ReadToEnd();
					var matches = Regex.Match(output, @"^[a-z0-9:]*\s*(([0-9-]{2}-){5}[0-9-]{2})", RegexOptions.Multiline);
					if (matches.Success && matches.Groups.Count > 1)
					{
						return PhysicalAddress.Parse(matches.Groups[1].Value);
					}
					return null;
				}

				//check what family the ip is from <cref="http://msdn.microsoft.com/en-us/library/system.net.sockets.addressfamily.aspx"/>
				if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
				{
					throw new ArgumentException("The remote system only supports IPv4 addresses");
				}

				var convertedIp = BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0);
				
				uint srcIp = 0;
				var macByteArray = new byte[6];
				int length = macByteArray.Length;

				//call the Win32 API SendARP <cref="http://msdn.microsoft.com/en-us/library/aa366358%28VS.85%29.aspx"/>
				int arpReply = SendARP(convertedIp, srcIp, macByteArray, ref length);

				if (arpReply != NO_ERROR)
				{
					switch (arpReply)
					{
						case ERROR_GEN_FAILURE:
							throw new Exception("A device attached to the system is not functioning. This error is returned on Windows Server 2003 and earlier when an ARP reply to the SendARP request was not received. This error can occur if destination IPv4 address could not be reached because it is not on the same subnet or the destination computer is not operating.");
						case ERROR_INVALID_PARAMETER:
							throw new Exception("One of the parameters is invalid. This error is returned on Windows Server 2003 and earlier if either the pMacAddr or PhyAddrLen parameter is a NULL pointer.");
						case ERROR_BAD_NET_NAME:
							throw new Exception("The network name cannot be found. This error is returned on Windows Vista and later when an ARP reply to the SendARP request was not received. This error occurs if the destination IPv4 address could not be reached.");
						case ERROR_BUFFER_OVERFLOW:
							throw new Exception("The file name is too long. This error is returned on Windows Vista if the ULONG value pointed to by the PhyAddrLen parameter is less than 6, the size required to store a complete physical address.");
						default:
							throw new Win32Exception(arpReply);
					}
				}

				result = new PhysicalAddress(macByteArray);
			}
			catch //(Exception ex)
			{ }
			return result;
		}

		private static string MacAddressToString(PhysicalAddress physicalAddress, char separator = ':')
		{
			string mac = physicalAddress.ToString();
			var friendlyMacAddress = new StringBuilder();
			for (int i = 0; i < mac.Length; i += 2)
			{
				if (i > 0) friendlyMacAddress.Append(separator);
				friendlyMacAddress.Append(mac[i]);
				friendlyMacAddress.Append(mac[i + 1]);
			}

			return friendlyMacAddress.ToString();
		}
	}
}
