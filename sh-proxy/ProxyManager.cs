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
        
        public void StartSshAndSocksProxy()
        {
            this.StartSshClient();
            this.CreateSocksFromSsh();
        }

        public void StopSshAndSocksProxy()
        {
            this.StopSocks();
            this.StopSshClient();

            this.IsSocksProxyEnabled = false;
        }

        public void StartSshClient()
        {
            string sshHost = "localhost";
            int sshPort = 22;
            string sshUsername = "ubuntu";
            string sshPassword = "admin";

            this.sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword);
            this.sshClient.Connect();
        }

        public void CreateSocksFromSsh()
        {
            this.socksProxy = new ForwardedPortDynamic("127.0.0.1", 1337);
            this.sshClient.AddForwardedPort(socksProxy);
            this.socksProxy.Start();

            this.IsSocksProxyEnabled = true;
        }

        private void StopSocks() => this.socksProxy.Stop();
        private void StopSshClient() => this.sshClient.Disconnect();
        private static void StopWithAppExcept() => throw new ApplicationException("Error starting socks/http proxy");
    }
}
