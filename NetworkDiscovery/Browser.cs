using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetworkDiscovery
{
    public static class Browser
    {
        public static void Start(string item)
        {
            try
            {
                Process.Start($"http://{item}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
