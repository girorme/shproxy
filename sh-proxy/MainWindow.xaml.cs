using System.Windows;
using System.Windows.Media;
using sh_proxy.Properties;

namespace sh_proxy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProxyManager proxyManager;
        private readonly ConfigManager configManager;

        public MainWindow()
        {
            InitializeComponent();
            configManager = new ConfigManager();
            proxyManager = new ProxyManager(configManager);

            FillInputs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Proxy has been upgraded to http...");
        }

        private void StartSocks_Click(object sender, RoutedEventArgs e)
        {
            if (proxyManager.IsSocksProxyEnabled)
            {
                StopSshAndSocksProxy();
                return;
            }

            StartSshAndSocksProxy();
        }

        private void StartHttp_Click(object sender, RoutedEventArgs e)
        {
            if (proxyManager.IsHttpProxyEnabled)
            {
                StopHttpProxy();
                return;
            }

            StartHttpProxy();
        }

        private void StartSshAndSocksProxy()
        {
            proxyManager.StartSshAndSocksProxy();
            startSocksProxyBtn.Content = "Stop";
            labelSocksProxyStatus.Text = "Enabled";
            labelSocksProxyStatus.Foreground = Brushes.GreenYellow;

            startHttpProxyBtn.IsEnabled = true;
        }

        private void StartHttpProxy()
        {
            proxyManager.StartHttpProxy();
            startHttpProxyBtn.Content = "Stop";
            labelHttpProxyStatus.Text = "Enabled";
            labelHttpProxyStatus.Foreground = Brushes.GreenYellow;
        }

        private void StopSshAndSocksProxy()
        {
            proxyManager.StopSshAndSocksProxy();
            startSocksProxyBtn.Content = "Start";
            labelSocksProxyStatus.Text = "Disabled";

            SolidColorBrush foregroundBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#FFFF9504");
            labelSocksProxyStatus.Foreground = foregroundBrush;

            StopHttpProxy();
        }

        private void StopHttpProxy()
        {
            proxyManager.StopHttpProxy();
            startHttpProxyBtn.IsEnabled = false;
            startHttpProxyBtn.Content = "Start";
            labelHttpProxyStatus.Text = "Disabled";

            SolidColorBrush foregroundBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#FFFF9504");
            labelHttpProxyStatus.Foreground = foregroundBrush;
        }

        private void FillInputs()
        {
            Settings.Default.Reload();

            sshServerInput.Text = Settings.Default.sshServer;
            sshUsernameInput.Text = Settings.Default.sshUsername;
            sshPasswordInput.Password = Settings.Default.sshPassword;
            proxyPortSocksInput.Text = Settings.Default.proxyPortSocks.ToString();
            proxyPortHttpInput.Text = Settings.Default.proxyPortHttp.ToString();

            SaveProperties();
        }

        private void SaveProperties()
        {
            configManager.SshServer = sshServerInput.Text;
            configManager.SshUsername = sshUsernameInput.Text;
            configManager.SshPassword = sshPasswordInput.Password;
            configManager.ProxyPortSocks = uint.Parse(proxyPortSocksInput.Text);
            configManager.ProxyPortHttp = uint.Parse(proxyPortHttpInput.Text);

            Settings.Default.sshServer = sshServerInput.Text;
            Settings.Default.sshUsername = sshUsernameInput.Text;
            Settings.Default.sshPassword = sshPasswordInput.Password;
            Settings.Default.proxyPortSocks = proxyPortSocksInput.Text;
            Settings.Default.proxyPortHttp = proxyPortHttpInput.Text;

            Settings.Default.Save();
        }

        private void SaveConfig_Click(object sender, RoutedEventArgs e) => SaveProperties();
    }
}
