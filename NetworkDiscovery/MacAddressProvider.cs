using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NetworkDiscovery
{
    public static class MacAddressProvider
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint DestIP, uint SrcIP, byte[] pMacAddr, ref int PhyAddrLen);


        private const int NO_ERROR = 0;
        private const int ERROR_BAD_NET_NAME = 67;
        private const int ERROR_BUFFER_OVERFLOW = 111;
        private const int ERROR_GEN_FAILURE = 31;
        private const int ERROR_INVALID_PARAMETER = 87;

        public static PhysicalAddress GetByIpAddress(IPAddress ipAddress)
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

                // Check what family the ip is from <cref="http://msdn.microsoft.com/en-us/library/system.net.sockets.addressfamily.aspx"/>
                if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
                {
                    throw new ArgumentException("The remote system only supports IPv4 addresses");
                }

                var convertedIp = BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0);

                uint srcIp = 0;
                var macByteArray = new byte[6];
                int length = macByteArray.Length;

                // Call the Win32 API SendARP <cref="http://msdn.microsoft.com/en-us/library/aa366358%28VS.85%29.aspx"/>
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
            catch (Exception ex)
            {
                //Debug.WriteLine($"MacAddressProvider error: {ex.Message}");
            }
            return result;
        }
    }
}
