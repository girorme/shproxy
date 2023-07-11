using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace via_proxy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread? proxyThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Proxy has been upgraded to http...");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProxyManager pm = new();
            
            // Extract thread logic to "launcher"
            proxyThread = new Thread(new ThreadStart(pm.StartSshAndSocksProxy));
            proxyThread.Start();

            
            startSocksProxyBtn.Content = "Stop";

            labelSocksProxyStatus.Text = "Enabled";
            labelSocksProxyStatus.Foreground = Brushes.GreenYellow;
        }
    }
}
