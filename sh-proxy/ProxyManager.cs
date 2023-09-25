using System;
using Renci.SshNet;
using System.IO;
using System.Windows;

namespace sh_proxy
{
    class ProxyManager
    {
        public bool IsSocksProxyEnabled { get; set; }
        public bool IsHttpProxyEnabled { get; set; }

        private SshClient? sshClient;
        private ForwardedPortDynamic? socksProxy;
        private CommandRunner? commandRunner;
        private ConfigManager? configManager;

        public ProxyManager(ConfigManager configManager)
        {
            this.configManager = configManager;
        }
        
        public bool StartSshAndSocksProxy()
        {
            try
            {
                StartSshClient();
                CreateSocksFromSsh();

                return true;
            } catch (System.Net.Sockets.SocketException e)
            {
                MessageBox.Show($"Unable to connect to ssh: {e.Message}", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public void StartHttpProxy()
        {
            commandRunner = new CommandRunner();
            commandRunner.StartCommand($".\\sthp -p {configManager.ProxyPortHttp} -s 127.0.0.1:{configManager.ProxyPortSocks}");
            IsHttpProxyEnabled = true;
        }

        public void StopHttpProxy()
        {
            if (commandRunner is null) return;

            commandRunner.StopCommand();
            IsHttpProxyEnabled = false;
        }

        public void StopSshAndSocksProxy()
        {
            StopSocks();
            StopSshClient();

            IsSocksProxyEnabled = false;
        }

        public void StartSshClient()
        {
            if (configManager is null) return;

            string sshHost = configManager.SshServer;
            int sshPort = 22;
            string sshUsername = configManager.SshUsername;
            string sshPassword = configManager.SshPassword;

            sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword);
            sshClient.Connect();
        }

        public void CreateSocksFromSsh()
        {
            if (configManager is null || sshClient is null) return;

            socksProxy = new ForwardedPortDynamic("127.0.0.1", configManager.ProxyPortSocks);
            sshClient.AddForwardedPort(socksProxy);
            socksProxy.Start();
            IsSocksProxyEnabled = true;
        }

        private void StopSocks()
        {
            if (socksProxy is null) return;
            socksProxy.Stop();
        }

        private void StopSshClient()
        {
            if (sshClient is null) return;
            sshClient.Disconnect();
        }
    }
}
