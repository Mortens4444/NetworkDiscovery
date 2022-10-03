using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDiscovery
{
	public partial class MainForm : Form
	{
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
                    ListViewItemBuilder.AddItemIfAvailable(ip.ToString(), lvDevices);
				}
				else
				{
					var subnet = ip.ToString().Substring(0, ip.ToString().LastIndexOf('.'));

                    for (int i = 1; i < 255; i++)
					{
						var temp = i;
                        Task.Factory.StartNew(() =>
						{
							var ipAddress = $"{subnet}.{temp}";
                            ListViewItemBuilder.AddItemIfAvailable(ipAddress, lvDevices);
                        });
					}
				}
			}
		}

		private void TsmiOpenWithHttp_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lvDevices.SelectedItems)
			{
                Browser.Start("http", item.Text);
			}
		}

		private void TsmiOpenWithHttps_Click(object sender, EventArgs e)
		{
            foreach (ListViewItem item in lvDevices.SelectedItems)
            {
                Browser.Start("https", item.Text);
            }
        }
    }
}
