using System;
using Renci.SshNet;

namespace sh_proxy
{
    class ProxyManager
    {
        public const bool OFF = false;
        public const bool ON = true;
        public bool IsSocksProxyEnabled { get; set; }
        public bool IsHttpProxyEnabled { get; set; }

        private SshClient? sshClient;
        private ForwardedPortDynamic? socksProxy;
        private CommandRunner? commandRunner;
        private ConfigManager configManager;

        public ProxyManager(ConfigManager configManager)
        {
            this.configManager = configManager;
        }
        
        public void StartSshAndSocksProxy()
        {
            this.StartSshClient();
            this.CreateSocksFromSsh();
        }

        public void StartHttpProxy()
        {
            // TODO: Add executable to solution
            commandRunner = new CommandRunner();
            commandRunner.StartCommand($"R:\\socks-to-http-proxy-main\\socks-to-http-proxy-main\\target\\release\\sthp -p {configManager.ProxyPortHttp} -s 127.0.0.1:{configManager.ProxyPortSocks}");
            this.IsHttpProxyEnabled = true;
        }

        public void StopHttpProxy()
        {
            commandRunner.StopCommand();
            this.IsHttpProxyEnabled = false;
        }

        public void StopSshAndSocksProxy()
        {
            this.StopSocks();
            this.StopSshClient();

            this.IsSocksProxyEnabled = false;
        }

        public void StartSshClient()
        {
            string sshHost = configManager.SshServer;
            int sshPort = 22;
            string sshUsername = configManager.SshUsername;
            string sshPassword = configManager.SshPassword;

            this.sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword);
            this.sshClient.Connect();
        }

        public void CreateSocksFromSsh()
        {
            this.socksProxy = new ForwardedPortDynamic("127.0.0.1", configManager.ProxyPortSocks);
            this.sshClient.AddForwardedPort(socksProxy);
            this.socksProxy.Start();

            this.IsSocksProxyEnabled = true;
        }

        private void StopSocks() => this.socksProxy.Stop();
        private void StopSshClient() => this.sshClient.Disconnect();
        private static void StopWithAppExcept() => throw new ApplicationException("Error starting socks/http proxy");
    }
}
