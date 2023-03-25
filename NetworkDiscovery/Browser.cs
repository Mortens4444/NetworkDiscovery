using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetworkDiscovery
{
    public static class Browser
    {
        public static void Start(string protocol, string item)
        {
            try
            {
                Process.Start($"{protocol}://{item}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Browser error: {ex.Message}");
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
