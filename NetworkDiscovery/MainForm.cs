using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDiscovery
{
	public partial class MainForm : Form
	{
        private const int Ipv4AddressRange = 255;
        private const int MaxConcurrentTasks = 50;

        public MainForm()
		{
			InitializeComponent();
		}

		private async Task Discovery()
		{
			var hostname = Dns.GetHostName();
			var ips = Dns.GetHostEntry(hostname);

			Invoke((Action)(() => tsProgressBar.Maximum = ips.AddressList.Length));
            foreach (var ip in ips.AddressList)
			{
				Invoke((Action)(() => tsslDiscoveryOn.Text = ip.ToString()));

                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
				{
                    var item = ListViewItemBuilder.Construct(ip.ToString());
					Invoke((Action)(() =>
					{
						tsProgressBar2.Maximum = 1;
						tsProgressBar2.Value = 0;
						ListViewItemAppender.AddToList(lvDevices, item);
						tsProgressBar2.Value++;
					}));
                }
				else
				{
					Invoke((Action)(() =>
					{
						tsProgressBar2.Maximum = Ipv4AddressRange - 1;
						tsProgressBar2.Value = 0;
					}));
                    var subnet = ip.ToString().Substring(0, ip.ToString().LastIndexOf('.'));
                    var semaphore = new SemaphoreSlim(MaxConcurrentTasks);

					var tasks = new List<Task>();
                    for (int i = 1; i < Ipv4AddressRange; i++)
					{
						var temp = i;
						var ipAddress = $"{subnet}.{temp}";
                        await semaphore.WaitAsync();

                        tasks.Add(Task.Run(() =>
						{
							var item = ListViewItemBuilder.Construct(ipAddress);
							ListViewItemAppender.AddToList(lvDevices, item);
							semaphore.Release();
							Invoke((Action)(() =>
							{
								tsslIpAddress.Text = ipAddress;
                                tsProgressBar2.Value++;
							}));
                        }));
                    }

                    await Task.WhenAll(tasks);
                }
				Invoke((Action)(() => tsProgressBar.Value++));
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

		private async void MainForm_Shown(object sender, EventArgs e)
		{
			await Discovery();
        }
    }
}
